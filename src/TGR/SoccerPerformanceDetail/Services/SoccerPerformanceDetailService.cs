namespace SoccerPerformanceDetail.Services;

using Fanex.Data.Repository;
using Criteria;
using Model;
using ViewModel;

public interface SoccerPerformanceDetailService
{
    Task<BetTypePerformanceList> GetBetTypePerformance(GetBetTypePerformanceCriteria criteria);

    Task<IEnumerable<GroupBetTypes>> GetGroupBetTypes(int userId, BettypeViewPermission permission);

    Task<IEnumerable<LeagueGroup>> GetLeagueGroups();
}

public class SoccerPerformanceDetailServiceImp(IDynamicRepository repository) : SoccerPerformanceDetailService
{
    public async Task<BetTypePerformanceList> GetBetTypePerformance(GetBetTypePerformanceCriteria criteria)
    {
        var betTypeData = await repository.FetchAsync<BetTypePerformanceInfo>(criteria);
        BetTypePerformanceList betTypePerformance = new BetTypePerformanceList();

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
}