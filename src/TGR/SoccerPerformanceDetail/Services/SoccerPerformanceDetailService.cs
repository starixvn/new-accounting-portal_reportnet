namespace SoccerPerformanceDetail.Services;

using AutoMapper;
using Criteria;
using Fanex.Data.Repository;
using global::Utilities.LinqDynamicSum;
using global::Utilities.RemotingRef;
using Model;
using ViewModel;

public interface SoccerPerformanceDetailService
{
    Task<BetTypePerformanceList> GetBetTypePerformance(GetBetTypePerformanceCriteria criteria);

    Task<IEnumerable<GroupBetTypes>> GetGroupBetTypes(int userId, BettypeViewPermission permission);

    Task<IEnumerable<LeagueGroup>> GetLeagueGroups();

    Task<IEnumerable<BetTypeItem>> GetBetTypes(int userId, BettypeViewPermission permission);

    Task<BetTypeBetChoiceList> GetBetTypeBetChoicePerformance(GetBetChoicePerformanceOfBetTypeCriteria criteria);

    Task<TraderPerformanceList> GetTraderPerformanceOfBetType(GetTraderPerformanceOfBetTypeCriteria criteria);

    Task<LeaguePerformanceList> GetLeaguePerformanceOfBetType(GetLeaguePerformanceOfBetTypeCriteria criteria);

    Task<LeaguePerformanceList> GetLeaguePerformanceOfBetChoice(GetLeaguePerformanceOfBetChoiceCriteria criteria);

    Task<MatchPerformanceList> GetMatchPerformanceOfTeamTrader(GetMatchPerformanceOfTeamTraderCriteria criteria);
    Task<MatchPerformanceList> GetMatchPerformanceOfBetTypeTrader(GetMatchPerformanceOfBetTypeTraderCriteria criteria);

    Task<TraderPerformanceList> GetTraderPerformanceOfBetTypeLeague(GetTraderPerformanceOfBetTypeLeagueCriteria criteria);
}

public class SoccerPerformanceDetailServiceImp(
    IDynamicRepository repository,
    IMapper mapper,
    RemotingRefService remotingRefService) : SoccerPerformanceDetailService
{
    public async Task<BetTypePerformanceList> GetBetTypePerformance(GetBetTypePerformanceCriteria criteria)
    {
        criteria.IsExcludeLeagues = (await GetLeagueExclusionConfig()).IsExcludeLeagues;
        var betTypeData = await repository.FetchAsync<BetTypePerformanceInfo>(criteria);
        var betTypePerformance = new BetTypePerformanceList();

        if (betTypeData != null && betTypeData.Any())
        {
            var data = SortBetTypePerformanceInfo(betTypeData);
            var itemByOderList = mapper.Map<List<BetTypePerformanceItem>>(data);

            var liveData = itemByOderList.Where(x => x.IsLive).ToList();
            var liveTotal = liveData.DynamicSum();

            var nonLiveData = itemByOderList.Where(x => !x.IsLive).ToList();
            var nonLiveTotal = nonLiveData.DynamicSum();

            var allTotal = itemByOderList.DynamicSum();

            if (criteria.MarketFilter == Market.All)
            {
                betTypePerformance.ItemByOrderList = itemByOderList;
            }
            else
            {
                var isLive = criteria.MarketFilter == Market.Live;
                betTypePerformance.ItemLiveList = liveData;
                betTypePerformance.ItemNonLiveList = nonLiveData;
            }

            betTypePerformance.TotalItemLive = liveTotal;
            betTypePerformance.TotalItemNonLive = nonLiveTotal;
            betTypePerformance.Total = allTotal;
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
                          .OrderByDescending(c => c.BetTypeId)
                          .ThenByDescending(c => c.IsLive)
                          .Select(c => new BetTypePerformanceInfo
                          {
                              BetTypeId = c.BetTypeId,
                              BetTypeName = c.BetTypeName,
                              NoOfTraders = c.NoOfTraders,
                              NoOfLeagues = c.NoOfLeagues,
                              NoOfMatches = c.NoOfMatches,
                              Turnover = c.Turnover,
                              WinLoss = c.WinLoss,
                              Margin = c.Margin,
                              TurnoverReal = c.TurnoverReal,
                              WinLossReal = c.WinLossReal,
                              MarginReal = c.MarginReal,
                              BetCount = c.BetCount,
                              IsLive = c.IsLive,
                              FlagForViewReport = betTypeData.Count(b => b.BetTypeId == c.BetTypeId)
                          });
    }

    public async Task<IEnumerable<BetTypeItem>> GetBetTypes(int userId, BettypeViewPermission permission)
    {
        List<BetTypeItem> betTypes = null;
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

        var (betChoiceData, betChoiceOfBetTypeInfo) = await repository.FetchMultipleAsync<BetTypeBetChoicePerformanceInfo, FMBetTypeBetChoiceInfo>(criteria);

        if (betChoiceOfBetTypeInfo != null && betChoiceOfBetTypeInfo.Any())
        {
            betTypeBetChoiceList.IsFMBetType = betChoiceOfBetTypeInfo.ToList()[0].IsFMBetType;
            betTypeBetChoiceList.IsBetChoiceDetail = betChoiceOfBetTypeInfo.ToList()[0].IsBetChoiceDetail;
        }
        var betChoiceList = mapper.Map<List<BetTypeBetChoiceItem>>(betChoiceData);

        betTypeBetChoiceList.ItemList = betChoiceList;

        var total = betChoiceList.DynamicSum();
        betTypeBetChoiceList.Total = total;

        return betTypeBetChoiceList;
    }

    public async Task<TraderPerformanceList> GetTraderPerformanceOfBetType(GetTraderPerformanceOfBetTypeCriteria criteria)
    {
        var rawData = (await repository.FetchAsync<TraderPerformanceInfo>(criteria)).ToList();

        var traderPerformance = CalculateBetTypeTraderPerformanceList(rawData);

        return traderPerformance;
    }

    public async Task<LeaguePerformanceList> GetLeaguePerformanceOfBetType(GetLeaguePerformanceOfBetTypeCriteria criteria)
    {
        var rawData = await repository.FetchAsync<LeaguePerformanceInfo>(criteria);

        var leaguePerformance = CalculatePerformanceLeague(rawData);

        return leaguePerformance;
    }

    public async Task<LeaguePerformanceList> GetLeaguePerformanceOfBetChoice(GetLeaguePerformanceOfBetChoiceCriteria criteria)
    {
        var rawData = await repository.FetchAsync<LeaguePerformanceInfo>(criteria);

        var leaguePerformance = CalculatePerformanceLeague(rawData);

        return leaguePerformance;
    }

    public async Task<MatchPerformanceList> GetMatchPerformanceOfTeamTrader(GetMatchPerformanceOfTeamTraderCriteria criteria)
    {
        var matchData = await repository.FetchAsync<MatchPerformanceInfo>(criteria);

        var matchPerformance = new MatchPerformanceList();

        if (matchData != null && matchData.Any())
        {
            var matchDataList = mapper.Map<List<MatchPerformanceItem>>(matchData);

            var leagueGroups = matchDataList.GroupBy(obj => obj.LeagueId);

            foreach (var group in leagueGroups)
            {
                var leagueGroup = CalculateMatchGroupByLeague(group.Key, matchDataList);
                matchPerformance.AddItem(leagueGroup);
            }

            var grandTotal = matchDataList.DynamicSum();

            matchPerformance.GroupList = matchPerformance.GroupList.OrderBy(obj => obj.LeagueName).ToList();
            matchPerformance.TotalItems = matchData.Count();
            matchPerformance.GrandTotal = grandTotal;
        }

        return matchPerformance;
    }

    private MatchPerformanceGroup CalculateMatchGroupByLeague(int leagueGroupId, List<MatchPerformanceItem> matchDataList)
    {
        var leagueGroupName = remotingRefService.GetLeagueName(leagueGroupId);

        var leagueGroup = new MatchPerformanceGroup();
        leagueGroup.LeagueName = leagueGroupName;

        var groupMatchByLeague = matchDataList.Where(x => x.LeagueId == leagueGroupId).ToList();
        var groupTotal = groupMatchByLeague.DynamicSum();

        var groupMatchLiveByLeague = matchDataList.Where(x => x.IsLive && x.LeagueId == leagueGroupId).ToList();

        var groupMatchNonLiveByLeague = matchDataList.Where(x => !x.IsLive && x.LeagueId == leagueGroupId).ToList();

        leagueGroup.ItemLiveList = CalculateMatchByLiveStatus(groupMatchLiveByLeague);

        leagueGroup.ItemNonLiveList = CalculateMatchByLiveStatus(groupMatchNonLiveByLeague);

        leagueGroup.Total = groupTotal;

        return leagueGroup;
    }

    private List<MatchPerformanceItem> CalculateMatchByLiveStatus(List<MatchPerformanceItem> data)
    {
        var matchByLiveStatus = new List<MatchPerformanceItem>();

        foreach (var item in data)
        {
            var homeTeam = remotingRefService.GetTeamName(item.HomeId);
            var awayTeam = remotingRefService.GetTeamName(item.AwayId);
            var matchName = $"{homeTeam} vs {awayTeam}";

            var eventDateTime = Convert.ToDateTime(item.EventDate);
            var eventDate = $"{eventDateTime: MM/dd/yyyy}";
            
            var match = mapper.Map<MatchPerformanceItem>(item);
            match.MatchName = matchName;
            match.EventDate = eventDate;

            matchByLiveStatus.Add(match);
        }

        return matchByLiveStatus;
    }

    public async Task<MatchPerformanceList> GetMatchPerformanceOfBetTypeTrader(GetMatchPerformanceOfBetTypeTraderCriteria criteria)
    {
        var matchData = await repository.FetchAsync<MatchPerformanceRawInfo>(criteria);

        var matchPerformance = new MatchPerformanceList();

        if (matchData != null && matchData.Any())
        {
            var matchDataList = mapper.Map<List<MatchPerformanceItem>>(matchData);
            
            var leagueGroups = matchDataList.GroupBy(obj => obj.LeagueId);

            foreach (var group in leagueGroups)
            {
                    var leagueGroup = CalculateMatchGroupByLeague(group.Key, matchDataList);
                    leagueGroup.Total.Score = leagueGroup.Total.SumCategory == 0 ? 
                    0 : 
                    leagueGroup.Total.WinLossScore * 100 / leagueGroup.Total.SumCategory;
                    matchPerformance.AddItem(leagueGroup);
            }

            var grandTotal = matchDataList.DynamicSum();

            matchPerformance.GroupList = matchPerformance.GroupList.OrderBy(obj => obj.LeagueName).ToList();
            matchPerformance.TotalItems = matchData.Count();
            matchPerformance.GrandTotal = grandTotal;
        }

        return matchPerformance;
    }

    private LeaguePerformanceList CalculatePerformanceLeague(IEnumerable<LeaguePerformanceInfo> rawData)
    {
        var leaguePerformance = new LeaguePerformanceList();

        if (rawData != null && rawData.Any())
        {
            var leaguePerformanceData = mapper.Map<List<LeaguePerformanceItem>>(rawData);

            var liveData = leaguePerformanceData.Where(x => x.IsLive).ToList();
            var liveTotal = liveData.DynamicSum();

            var nonLiveData = leaguePerformanceData.Where(x => !x.IsLive).ToList();
            var nonLiveTotal = nonLiveData.DynamicSum();

            var total = leaguePerformanceData.DynamicSum();

            leaguePerformance.ItemLiveList = liveData;
            leaguePerformance.ItemNonLiveList = nonLiveData;

            leaguePerformance.TotalItemLive = liveTotal;
            leaguePerformance.TotalItemNonLive = nonLiveTotal;

            leaguePerformance.Total = total;
        }

        return leaguePerformance;
    }

    public async Task<TraderPerformanceList> GetTraderPerformanceOfBetTypeLeague(GetTraderPerformanceOfBetTypeLeagueCriteria criteria)
    {
        var traderData = await repository.FetchAsync<TraderPerformanceInfo>(criteria);

        var traderPerformance = CalculateBetTypeTraderPerformanceList(traderData);
        
        return traderPerformance;
    }

    private TraderPerformanceList CalculateBetTypeTraderPerformanceList(IEnumerable<TraderPerformanceInfo> rawData)
    {
        var traderPerformance = new TraderPerformanceList();

        if (rawData != null && rawData.Any())
        {
            var data = mapper.Map<List<TraderPerformanceItem>>(rawData);

            var liveData = data.Where(x => x.IsLive).ToList();
            var nonLiveData = data.Where(x => !x.IsLive).ToList();

            traderPerformance.ItemLiveList = liveData;
            traderPerformance.ItemNonLiveList = nonLiveData;

            var total = data.DynamicSum();

            traderPerformance.Total = total;
        }

        return traderPerformance;
    }
}