namespace SoccerPerformanceDetail.Services;

using Fanex.Data.Repository;
using Criteria;
using Model;
using ViewModel;
using Microsoft.Extensions.DependencyInjection;

public interface SoccerPerformanceDetailService
{
    Task<BetTypePerformanceList> GetBetTypePerformance(GetBetTypePerformanceCriteria criteria);

    Task<IEnumerable<GroupBetTypes>> GetGroupBetTypes(int userId, BettypeViewPermission permission);

    Task<IEnumerable<LeagueGroup>> GetLeagueGroups();

    Task<IEnumerable<BetTypeItem>> GetBetTypes(int userId, BettypeViewPermission permission);

    Task<BetTypeBetChoiceList> GetBetTypeBetChoicePerformance(GetBetChoicePerformanceOfBetTypeCriteria criteria);
}

public class SoccerPerformanceDetailServiceImp(IDynamicRepository repository) : SoccerPerformanceDetailService
{
    public async Task<BetTypePerformanceList> GetBetTypePerformance(GetBetTypePerformanceCriteria criteria)
    {
        criteria.IsExcludeLeagues = (await GetLeagueExclusionConfig()).IsExcludeLeagues;
        var betTypeData = await repository.FetchAsync<BetTypePerformanceInfo>(criteria);
        var betTypePerformance = new BetTypePerformanceList();

        if (betTypeData != null && betTypeData.Any())
        {
            if (criteria.MarketFilter == Market.All)
            {
                var data = SortBetTypePerformanceInfo(betTypeData);

                betTypePerformance.AddBetTypeList(data);
            }
            else
            {
                betTypePerformance.AddBetTypeList(betTypeData, criteria.MarketFilter == Market.Live);
            }

            betTypePerformance.CalculateTotalMargin();
        }

        return betTypePerformance;
    }

    public async Task<IEnumerable<GroupBetTypes>> GetGroupBetTypes(int userId, BettypeViewPermission permission)
    {
        var groupBetTypes = Enumerable.Empty<GroupBetTypes>();
        var criteria = new GetBetTypesCriteria(permission) { UserId = userId };
        var data = await repository.FetchAsync<BetTypeInfo>(criteria);

        if (data != null && data.Any())
        {
            groupBetTypes = data
                .GroupBy(g => new { g.GroupBettypeId, g.GroupBettypeName })
                .Select(x => new { Group = x.Key, BetTypeList = x.ToList() })
                .Select(x => new GroupBetTypes
                {
                    GroupId = x.Group.GroupBettypeId,
                    GroupName = x.Group.GroupBettypeName,
                    TotalDefaultCheckOfBetTypeList = x.BetTypeList.Count(y => y.IsDefault),
                    BetTypeList = x.BetTypeList.Select(y => new BetType
                    {
                        BetTypeId = y.BetType,
                        BetTypeName = y.TypeName,
                        IsDefaultCheck = y.IsDefault
                    })
                });
        }

        return groupBetTypes;
    }

    public async Task<IEnumerable<LeagueGroup>> GetLeagueGroups()
    {
        var criteria = new GetLeagueGroupSoccerCriteria { LeagueGroupIds = string.Empty };

        var leagueGroupList = new List<LeagueGroup>();

        var data = await repository.FetchAsync<LeagueGroup>(criteria);

        if (data.Any())
        {
            leagueGroupList = data.DistinctBy(x => x.TargetLeagueGroupId).ToList();
        }

        return leagueGroupList;
    }

    private IEnumerable<BetTypePerformanceInfo> SortBetTypePerformanceInfo(IEnumerable<BetTypePerformanceInfo> betTypeData)
    {
        return betTypeData.AsEnumerable()
                          .OrderByDescending(c => c.BetType)
                          .ThenByDescending(c => c.IsLive)
                          .Select(c => new BetTypePerformanceInfo
                          {
                              BetType = c.BetType,
                              TypeName = c.TypeName,
                              TraderCount = c.TraderCount,
                              LeagueCount = c.LeagueCount,
                              MatchCount = c.MatchCount,
                              Turnover = c.Turnover,
                              WinLoss = c.WinLoss,
                              Margin = c.Margin,
                              TurnoverReal = c.TurnoverReal,
                              WinLossReal = c.WinLossReal,
                              MarginReal = c.MarginReal,
                              BetCount = c.BetCount,
                              IsLive = c.IsLive,
                              FlagForViewReport = betTypeData.Count(b => b.BetType == c.BetType)
                          });
    }

    public async Task<IEnumerable<BetTypeItem>> GetBetTypes(int userId, BettypeViewPermission permission)
    {
        List<BetTypeItem>? betTypes = null;
        var criteria = new GetBetTypesCriteria(permission) { UserId = userId };
        var data = await repository.FetchAsync<BetTypeInfo>(criteria);

        if (data != null && data.Any())
        {
            betTypes = data.Select(obj => new BetTypeItem(obj)).ToList();
        }

        return betTypes ?? [];
    }

    private async Task<LeagueExclusionConfig> GetLeagueExclusionConfig()
    {
        return await repository.GetAsync<LeagueExclusionConfig>(new LeagueExclusionConfigGetCriteria());
    }

    public async Task<BetTypeBetChoiceList> GetBetTypeBetChoicePerformance(GetBetChoicePerformanceOfBetTypeCriteria criteria)
    {
        var betTypeBetChoiceList = new BetTypeBetChoiceList();

        var rawData = await repository.FetchMultipleAsync<BetTypeBetChoicePerformanceInfo, FMBetTypeBetChoiceInfo>(criteria);

        var betChoiceData = rawData.Item1;
        var betChoiceOfBetTypeInfo = rawData.Item2;

        if (betChoiceOfBetTypeInfo != null && betChoiceOfBetTypeInfo.Any())
        {
            betTypeBetChoiceList.IsFMBetType = betChoiceOfBetTypeInfo.ToList()[0].IsFMBetType;
            betTypeBetChoiceList.IsBetChoiceDetail = betChoiceOfBetTypeInfo.ToList()[0].IsBetChoiceDetail;
        }

        betTypeBetChoiceList.AddItem(betChoiceData);

        return betTypeBetChoiceList;
    }
}