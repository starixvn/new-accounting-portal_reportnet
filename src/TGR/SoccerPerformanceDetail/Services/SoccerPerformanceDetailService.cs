namespace SoccerPerformanceDetail.Services;

using AutoMapper;
using Criteria;
using Fanex.Data.Repository;
using global::Utilities.LinqDynamicSum;
using global::Utilities.RemotingRef;
using Model;
using Org.BouncyCastle.Asn1.Pkcs;
using System.Collections.Generic;
using ViewModel;

public interface SoccerPerformanceDetailService
{
    Task<BetTypePerformanceList> GetBetTypePerformance(GetBetTypePerformanceCriteria criteria);

    Task<IEnumerable<GroupBetTypes>> GetGroupBetTypes(int userId, BettypeViewPermission permission);

    Task<IEnumerable<LeagueGroup>> GetLeagueGroups();

    Task<IEnumerable<TeamItem>> GetTeams();

    Task<IEnumerable<BetTypeItem>> GetBetTypes(int userId, BettypeViewPermission permission);

    Task<BetTypeBetChoiceList> GetBetTypeBetChoicePerformance(GetBetChoicePerformanceOfBetTypeCriteria criteria);

    Task<TraderPerformanceList> GetTraderPerformanceOfBetType(GetTraderPerformanceOfBetTypeCriteria criteria);

    Task<LeaguePerformanceList> GetLeaguePerformanceOfBetType(GetLeaguePerformanceOfBetTypeCriteria criteria);

    Task<LeaguePerformanceList> GetLeaguePerformanceOfBetChoice(GetLeaguePerformanceOfBetChoiceCriteria criteria);

    Task<MatchPerformanceList> GetMatchPerformanceOfTeamTrader(GetMatchPerformanceOfTeamTraderCriteria criteria);

    Task<MatchPerformanceList> GetMatchPerformanceOfBetTypeTrader(GetMatchPerformanceOfBetTypeTraderCriteria criteria);

    Task<TraderPerformanceList> GetTraderPerformanceOfBetTypeLeague(GetTraderPerformanceOfBetTypeLeagueCriteria criteria);

    Task<BetTypeBetChoiceList> GetBetChoicePerformanceOfBetTypeLeague(GetBetChoicePerformanceOfBetTypeLeagueCriteria criteria);

    Task<MatchPerformanceGroup> GetMatchPerformanceOfBetTypeLeagueTrader(GetMatchPerformanceOfBetTypeLeagueTraderCriteria criteria);

    Task<LeaguePerformanceList> GetLeaguePerformance(GetLeaguePerformanceCriteria criteria, bool isGroupOfLeague = false);

    Task<TraderPerformanceList> GetTraderPerformanceOfLeague(GetTraderPerformanceOfLeagueCriteria criteria);

    Task<MatchPerformanceGroup> GetMatchPerformanceOfLeagueTrader(GetMatchPerformanceOfLeagueTraderCriteria criteria);

    Task<MatchPerformanceGroup> GetMatchPerformanceOfLeague(GetMatchPerformanceOfLeagueCriteria criteria);

    Task<IEnumerable<BetTypeDetailItem>> GetBettypePerformanceOfLeagueMatch(
        GetBetTypePerformanceOfLeagueMatchCriteria criteria);

    Task<List<BetTypeDetailItem>> GetBettypeDetailLeague(GetBettypeDetailLeague criteria);

    Task<GradePerformanceList> GetGradePerformance(GetGradePerformanceCriteria criteria);

    Task<TraderPerformanceList> GetTraderPerformance(ICriteria criteria);

    Task<MatchPerformanceList> GetMatchPerformanceOfTrader(ICriteria criteria);

    Task<IEnumerable<BetTypeDetailItem>> GetBettypeDetailTrader(GetBettypeDetailTrader criteria);

    Task<TraderPerformanceList> GetTraderPerformanceOfGrade(GetTraderPerformanceOfGradeCriteria criteria);

    Task<LeaguePerformanceList> GetLeaguePerformanceOfGrade(GetLeaguePerformanceOfGradeCriteria criteria);

    Task<TeamPerformanceList> GetTeamPerformance(GetTeamPerformanceCriteria criteria);

    Task<TraderPerformanceList> GetTraderPerformanceOfTeam(GetTraderPerformanceOfTeamCriteria criteria);

    Task<LeaguePerformanceList> GetLeaguePerformance(GetLeagueDetailTeamCriteria criteria, bool isGroupOfLeague = false);
    
    Task<IEnumerable<BetTypeDetailItem>> GetBettypeDetailTeam(GetBettypeDetailTeamCriteria criteria);

    Task<MatchPerformanceList> GetMatchPerformanceOfGradeTrader(GetMatchPerformanceOfGradeTraderCriteria criteria);

    Task<IEnumerable<BetTypeDetailItem>> GetBettypeDetailGrade(GetBettypeDetailGradeCriteria criteria);

    Task<TraderPerformanceList> GetTraderPerformanceOfGradeByLeague(GetTraderPerformanceOfGradeByLeagueCriteria criteria);
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

        if (betTypeData == null || !betTypeData.Any())
        {
            return betTypePerformance;
        }

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
            betTypePerformance.ItemLiveList = liveData;
            betTypePerformance.ItemNonLiveList = nonLiveData;
        }

        betTypePerformance.TotalItemLive = liveTotal;
        betTypePerformance.TotalItemNonLive = nonLiveTotal;
        betTypePerformance.Total = allTotal;

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

    public Task<IEnumerable<TeamItem>> GetTeams()
    {
        return repository.FetchAsync<TeamItem>(new GetTeamsCriteria());
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
        var (betChoiceData, betChoiceOfBetTypeInfo) = await repository.FetchMultipleAsync<BetTypeBetChoicePerformanceInfo, FMBetTypeBetChoiceInfo>(criteria);

        var betTypeBetChoiceList = CalculateBetTypeBetChoiceList(betChoiceData, betChoiceOfBetTypeInfo);

        return betTypeBetChoiceList;
    }

    public async Task<TraderPerformanceList> GetTraderPerformanceOfBetType(GetTraderPerformanceOfBetTypeCriteria criteria)
    {
        var rawData = await repository.FetchAsync<TraderPerformanceInfo>(criteria);

        var traderPerformance = CalculateTraderPerformanceList(rawData);

        return traderPerformance;
    }

    public async Task<LeaguePerformanceList> GetLeaguePerformanceOfBetType(GetLeaguePerformanceOfBetTypeCriteria criteria)
    {
        var rawData = await repository.FetchAsync<LeaguePerformanceInfo>(criteria);

        var leaguePerformance = CalculatePerformanceLeague(rawData, false);

        return leaguePerformance;
    }

    public async Task<LeaguePerformanceList> GetLeaguePerformanceOfBetChoice(GetLeaguePerformanceOfBetChoiceCriteria criteria)
    {
        var rawData = await repository.FetchAsync<LeaguePerformanceInfo>(criteria);

        var leaguePerformance = CalculatePerformanceLeague(rawData, false);

        return leaguePerformance;
    }

    public async Task<MatchPerformanceList> GetMatchPerformanceOfTeamTrader(GetMatchPerformanceOfTeamTraderCriteria criteria)
    {
        var rawData = await repository.FetchAsync<MatchPerformanceInfo>(criteria);

        var matchPerformance = CalculateMatchPerformanceByLeague(rawData);

        return matchPerformance;
    }

    private MatchPerformanceList CalculateMatchPerformanceByLeague(IEnumerable<MatchPerformanceInfo> rawData)
    {
        var matchPerformance = new MatchPerformanceList();

        if (rawData == null || !rawData.Any())
        {
            return matchPerformance;
        }

        var matchDataList = mapper.Map<List<MatchPerformanceItem>>(rawData);

        var leagueGroups = matchDataList.GroupBy(obj => obj.LeagueId);

        foreach (var group in leagueGroups)
        {
            var leagueGroup = CalculateMatchGroupByLeague(group.Key, matchDataList);
            matchPerformance.AddItem(leagueGroup);
        }

        var grandTotal = matchDataList.DynamicSum();

        matchPerformance.GroupList = matchPerformance.GroupList.OrderBy(obj => obj.LeagueName).ToList();
        matchPerformance.TotalItems = rawData.Count();
        matchPerformance.GrandTotal = grandTotal;

        return matchPerformance;
    }

    private MatchPerformanceGroup CalculateMatchGroupByLeague(int leagueGroupId, List<MatchPerformanceItem> matchDataList)
    {
        var leagueGroupName = remotingRefService.GetLeagueName(leagueGroupId);

        var leagueGroup = new MatchPerformanceGroup
        {
            LeagueName = leagueGroupName
        };

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

        if (matchData == null || !matchData.Any())
        {
            return matchPerformance;
        }

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

        return matchPerformance;
    }

    private LeaguePerformanceList CalculatePerformanceLeague(IEnumerable<LeaguePerformanceInfo> rawData, bool isAllMarket)
    {
        var leaguePerformance = new LeaguePerformanceList();

        if (rawData == null || !rawData.Any())
        {
            return leaguePerformance;
        }

        var leaguePerformanceData = mapper.Map<List<LeaguePerformanceItem>>(rawData);

        var liveData = leaguePerformanceData.Where(x => x.IsLive).ToList();
        var liveTotal = liveData.DynamicSum();

        var nonLiveData = leaguePerformanceData.Where(x => !x.IsLive).ToList();
        var nonLiveTotal = nonLiveData.DynamicSum();

        var total = leaguePerformanceData.DynamicSum();

        if (isAllMarket)
        {
            leaguePerformance.ItemByOrderList = leaguePerformanceData;
        }
        else
        {
            leaguePerformance.ItemLiveList = liveData;
            leaguePerformance.ItemNonLiveList = nonLiveData;
        }

        leaguePerformance.TotalItemLive = liveTotal;
        leaguePerformance.TotalItemNonLive = nonLiveTotal;

        leaguePerformance.Total = total;

        return leaguePerformance;
    }

    public async Task<TraderPerformanceList> GetTraderPerformanceOfBetTypeLeague(GetTraderPerformanceOfBetTypeLeagueCriteria criteria)
    {
        var traderData = await repository.FetchAsync<TraderPerformanceInfo>(criteria);

        var traderPerformance = CalculateTraderPerformanceList(traderData);
        
        return traderPerformance;
    }

    private TraderPerformanceList CalculateTraderPerformanceList(IEnumerable<TraderPerformanceInfo> rawData)
    {
        var traderPerformance = new TraderPerformanceList();

        if (rawData == null || !rawData.Any())
        {
            return traderPerformance;
        }

        var data = mapper.Map<List<TraderPerformanceItem>>(rawData);

        var liveData = data.Where(x => x.IsLive).ToList();
        var nonLiveData = data.Where(x => !x.IsLive).ToList();

        traderPerformance.ItemLiveList = liveData;
        traderPerformance.ItemNonLiveList = nonLiveData;

        var total = data.DynamicSum();

        traderPerformance.Total = total;

        return traderPerformance;
    }

    public async Task<BetTypeBetChoiceList> GetBetChoicePerformanceOfBetTypeLeague(GetBetChoicePerformanceOfBetTypeLeagueCriteria criteria)
    {
        var (betChoiceData, betChoiceOfBetTypeInfo) = await repository.FetchMultipleAsync<BetTypeBetChoicePerformanceInfo, FMBetTypeBetChoiceInfo>(criteria);

        var betTypeBetChoiceList = CalculateBetTypeBetChoiceList(betChoiceData, betChoiceOfBetTypeInfo);

        return betTypeBetChoiceList;
    }

    private BetTypeBetChoiceList CalculateBetTypeBetChoiceList(
        IEnumerable<BetTypeBetChoicePerformanceInfo> betChoiceData,
        IEnumerable<FMBetTypeBetChoiceInfo> betChoiceOfBetTypeInfo)
    {
        var betTypeBetChoiceList = new BetTypeBetChoiceList();

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

    public async Task<MatchPerformanceGroup> GetMatchPerformanceOfBetTypeLeagueTrader(GetMatchPerformanceOfBetTypeLeagueTraderCriteria criteria)
    {
        var rawData = await repository.FetchAsync<MatchPerformanceInfo>(criteria);

        return CalculateMatchPerformance(rawData);
    }

    private MatchPerformanceGroup CalculateMatchPerformance(IEnumerable<MatchPerformanceInfo> rawData)
    {
        var matchPerformance = new MatchPerformanceGroup();

        if (rawData == null || !rawData.Any())
        {
            return matchPerformance;
        }

        var matches = mapper.Map<List<MatchPerformanceItem>>(rawData);

        var liveMatches = matches.Where(x => x.IsLive).ToList();
        var nonLiveMatches = matches.Where(x => !x.IsLive).ToList();

        var total = matches.DynamicSum();

        matchPerformance.ItemLiveList = CalculateMatchByLiveStatus(liveMatches);
        matchPerformance.ItemNonLiveList = CalculateMatchByLiveStatus(nonLiveMatches);
        matchPerformance.Total = total;

        return matchPerformance;
    }

    public async Task<LeaguePerformanceList> GetLeaguePerformance(GetLeaguePerformanceCriteria criteria, bool isGroupOfLeague = false)
    {
        var rawData = await repository.FetchAsync<LeaguePerformanceInfo>(criteria);

        var leaguePerformance = new LeaguePerformanceList();

        if (rawData != null && rawData.Any())
        {
            leaguePerformance = CalculateLeaguePerformanceByGroup(rawData, isGroupOfLeague, criteria.MarketFilter == Market.All);
        }

        return leaguePerformance;
    }

    private LeaguePerformanceList CalculateLeaguePerformanceByGroup(IEnumerable<LeaguePerformanceInfo> rawData, bool isGroupOfLeague, bool isAllMarket)
    {
        if (!isGroupOfLeague)
        {
            return CalculatePerformanceLeague(rawData, isAllMarket);
        }

        var leagueData = rawData
            .OrderBy(c => c.LeagueId)
            .ThenByDescending(c => c.IsLive)
            .Select(c => new LeaguePerformanceInfo
            {
                LeagueId = c.LeagueId,
                LeagueName = c.LeagueName,
                LeagueGroupID = c.LeagueGroupID,
                LeagueBenchmark = c.LeagueBenchmark,
                BetTypeBMShift1 = c.BetTypeBMShift1,
                BetTypeBMShift2 = c.BetTypeBMShift2,
                BetTypeBMShift3 = c.BetTypeBMShift3,
                NoOfTraders = c.NoOfTraders,
                NoOfMatches = c.NoOfMatches,
                Turnover = c.Turnover,
                WinLoss = c.WinLoss,
                Margin = c.Margin,
                TurnoverReal = c.TurnoverReal,
                WinLossReal = c.WinLossReal,
                MarginReal = c.MarginReal,
                BetCount = c.BetCount,
                Market = c.Market,
                IsLive = c.IsLive,
                FlagForViewReport = rawData.Count(b => b.LeagueId == c.LeagueId)
            });

        return CalculatePerformanceLeague(leagueData, isAllMarket);

    }

    public async Task<TraderPerformanceList> GetTraderPerformanceOfLeague(GetTraderPerformanceOfLeagueCriteria criteria)
    {
        var  rawData = await repository.FetchAsync<TraderPerformanceInfo>(criteria);

        return CalculateTraderPerformanceList(rawData);
    }

    public async Task<MatchPerformanceGroup> GetMatchPerformanceOfLeagueTrader(GetMatchPerformanceOfLeagueTraderCriteria criteria)
    {
        var rawData = await repository.FetchAsync<MatchPerformanceInfo>(criteria);

        return CalculateMatchPerformance(rawData);
    }

    public async Task<MatchPerformanceGroup> GetMatchPerformanceOfLeague(GetMatchPerformanceOfLeagueCriteria criteria)
    {
        var rawData = await repository.FetchAsync<MatchPerformanceInfo>(criteria);

        var matchPerformance = CalculateMatchPerformance(rawData);

        return matchPerformance;
    }

    public async Task<IEnumerable<BetTypeDetailItem>> GetBettypePerformanceOfLeagueMatch(GetBetTypePerformanceOfLeagueMatchCriteria criteria)
    {
        var rawData = await repository.FetchAsync<BetTypeDetailInfo>(criteria);
        var bettypePerformance = mapper.Map<List<BetTypeDetailItem>>(rawData);
        return bettypePerformance;
    }

    public async Task<List<BetTypeDetailItem>> GetBettypeDetailLeague(GetBettypeDetailLeague criteria)
    {
        var rawData = await repository.FetchAsync<BetTypeDetailInfo>(criteria);

        var scoreByPeriodCriteria = new GetScoreByPeriodCriteria
        {
            FromDate = criteria.FromDate,
            ToDate = criteria.ToDate,
            CustId = criteria.CustId,
            MatchId = criteria.MatchId,
            Bettypes = string.Join(",", rawData.Select(b => b.BetType.ToString())),
            IsExcludeLeagues = true,
            LeagueGroupIds = criteria.LeagueGroupIds
        };

        var bettypeDetail = await CalculateBetType(rawData, criteria.IsLive, scoreByPeriodCriteria);

        return bettypeDetail;
    }

    private async Task<List<BetTypeDetailItem>> CalculateBetType(
        IEnumerable<BetTypeDetailInfo> rawData,
        bool isLive,
        GetScoreByPeriodCriteria scoreByPeriodCriteria)
    {
        var bettypeDetail = new List<BetTypeDetailItem>();

        if (rawData == null || !rawData.Any())
        {
            return bettypeDetail;
        }

        if (isLive)
        {
            var scoreByPeriodData = await repository.FetchAsync<ScoreByPeriodInfo>(scoreByPeriodCriteria);

            bettypeDetail = CalculateLiveBetTypeDetail(rawData, scoreByPeriodData);
        }
        else
        {
            bettypeDetail = mapper.Map<List<BetTypeDetailItem>>(rawData);
        }

        return bettypeDetail;
    }

    private List<BetTypeDetailItem> CalculateLiveBetTypeDetail(
        IEnumerable<BetTypeDetailInfo> rawData,
        IEnumerable<ScoreByPeriodInfo> scorePeriodRawData)
    {
        var result = new List<BetTypeDetailItem>();

        foreach (var rawItem in rawData)
        {
            var item = mapper.Map<BetTypeDetailItem>(rawItem);
            item.Score = scorePeriodRawData.First(s => s.BetType == rawItem.BetType).Score;
            item.ScoreByPeriod = scorePeriodRawData.Where(s => s.BetType == rawItem.BetType).Select(i => new ScoreByPeriodItem(i)).ToList();

            result.Add(item);
        }

        return result;
    }

    public async Task<GradePerformanceList> GetGradePerformance(GetGradePerformanceCriteria criteria)
    {
        var rawData = await repository.FetchAsync<GradePerformanceInfo>(criteria);
        
        var gradePerformance = new GradePerformanceList();

        if (rawData == null || !rawData.Any())
        {
            return gradePerformance;
        }

        var gradeData = mapper.Map<List<GradePerformanceItem>>(rawData);

        var gradePerformanceLive = gradeData.Where(x => x.IsLive).ToList();
        var gradePerformanceNonLive = gradeData.Where(x => !x.IsLive).ToList();
        var total = gradeData.DynamicSum();

        gradePerformance.ItemLiveList = gradePerformanceLive;
        gradePerformance.ItemNonLiveList = gradePerformanceNonLive;
        gradePerformance.Total = total;

        return gradePerformance;
    }

    public async Task<TraderPerformanceList> GetTraderPerformance(ICriteria criteria)
    {
        var rawData = await repository.FetchAsync<TraderPerformanceInfo>(criteria);

        var traderPerformance = CalculateTraderPerformanceList(rawData);
        
        return traderPerformance;
    }

    public async Task<MatchPerformanceList> GetMatchPerformanceOfTrader(ICriteria criteria)
    {
        var rawData = await repository.FetchAsync<MatchPerformanceInfo>(criteria);

        var matchPerformance = CalculateMatchPerformanceByLeague(rawData);
        
        return matchPerformance;
    }

    public async Task<IEnumerable<BetTypeDetailItem>> GetBettypeDetailTrader(GetBettypeDetailTrader criteria)
    {
        var rawData = await repository.FetchAsync<BetTypeDetailInfo>(criteria);

        var scoreByPeriodCriteria = new GetScoreByPeriodCriteria
        {
            FromDate = criteria.FromDate,
            ToDate = criteria.ToDate,
            CustId = criteria.CustId,
            MatchId = criteria.MatchId,
            Bettypes = string.Join(",", rawData.Select(b => b.BetType.ToString())),
            IsExcludeLeagues = true,
            LeagueGroupIds = criteria.LeagueGroupIds
        };

        var bettypeDetail = await CalculateBetType(rawData, criteria.IsLive, scoreByPeriodCriteria);

        return bettypeDetail;
    }

    public async Task<TraderPerformanceList> GetTraderPerformanceOfGrade(GetTraderPerformanceOfGradeCriteria criteria)
    {
        var traderData = await repository.FetchAsync<TraderPerformanceInfo>(criteria);
        
        var traderPerformance = CalculateTraderPerformanceList(traderData);

        return traderPerformance;
    }

    public async Task<LeaguePerformanceList> GetLeaguePerformanceOfGrade(GetLeaguePerformanceOfGradeCriteria criteria)
    {
        var leagueData = await repository.FetchAsync<LeaguePerformanceInfo>(criteria);
        var leaguePerformance = CalculatePerformanceLeague(leagueData, false);

        return leaguePerformance;
    }

    public async Task<TeamPerformanceList> GetTeamPerformance(GetTeamPerformanceCriteria criteria)
    {
        var rawData = await repository.FetchAsync<TeamPerformanceInfo>(criteria);

        var teamPerformance = new TeamPerformanceList();

        if (rawData == null || !rawData.Any())
        {
            return teamPerformance;
        }

        var teamData = mapper.Map<List<TeamPerformanceItem>>(rawData);

        var teamPerformanceNonLive = teamData.Where(x => !x.IsLive).ToList();
        var teamPerformanceLive = teamData.Where(x => x.IsLive).ToList();
        var total = teamData.DynamicSum();

        teamPerformance.ItemLiveList = teamPerformanceLive;
        teamPerformance.ItemNonLiveList = teamPerformanceNonLive;
        teamPerformance.Total = total;

        return teamPerformance;
    }

    public async Task<TraderPerformanceList> GetTraderPerformanceOfTeam(GetTraderPerformanceOfTeamCriteria criteria)
    {
        var rawData = await repository.FetchAsync<TraderPerformanceInfo>(criteria);

        var traderPerformance = CalculateTraderPerformanceList(rawData);

        return traderPerformance;
    }

    public async Task<LeaguePerformanceList> GetLeaguePerformance(GetLeagueDetailTeamCriteria criteria, bool isGroupOfLeague = false)
    {
        var rawData = await repository.FetchAsync<LeaguePerformanceInfo>(criteria);

        var leaguePerformance = new LeaguePerformanceList();

        if (rawData != null && rawData.Any())
        {
            leaguePerformance = CalculateLeaguePerformanceByGroup(rawData, isGroupOfLeague, false);
        }

        return leaguePerformance;
    }

    public async Task<IEnumerable<BetTypeDetailItem>> GetBettypeDetailTeam(GetBettypeDetailTeamCriteria criteria)
    {
        var rawData = await repository.FetchAsync<BetTypeDetailInfo>(criteria);

        var scoreByPeriodCriteria = new GetScoreByPeriodCriteria
        {
            FromDate = criteria.FromDate,
            ToDate = criteria.ToDate,
            CustId = criteria.CustId,
            MatchId = criteria.MatchId,
            Bettypes = string.Join(",", rawData.Select(b => b.BetType.ToString())),
            IsExcludeLeagues = true,
            LeagueGroupIds = criteria.LeagueGroupIds
        };

        var bettypeDetail = await CalculateBetType(rawData, criteria.IsLive, scoreByPeriodCriteria);

        return bettypeDetail;
    }

    public async Task<MatchPerformanceList> GetMatchPerformanceOfGradeTrader(GetMatchPerformanceOfGradeTraderCriteria criteria)
    {
        var rawData = await repository.FetchAsync<MatchPerformanceInfo>(criteria);
        var matchPerformance = new MatchPerformanceList();

        if (rawData == null || !rawData.Any())
        {
            return matchPerformance;
        }

        var matchDataList = mapper.Map<List<MatchPerformanceItem>>(rawData);

        var leagueGroups = matchDataList.GroupBy(obj => obj.LeagueId);

        foreach (var group in leagueGroups)
        {
            var leagueGroup = CalculateMatchGroupByLeague(group.Key, matchDataList);
            matchPerformance.AddItem(leagueGroup);
        }

        var grandTotal = matchDataList.DynamicSum();

        matchPerformance.GroupList = matchPerformance.GroupList.OrderBy(obj => obj.LeagueName).ToList();
        matchPerformance.TotalItems = rawData.Count();
        matchPerformance.GrandTotal = grandTotal;

        return matchPerformance;


    }

    public async Task<IEnumerable<BetTypeDetailItem>> GetBettypeDetailGrade(GetBettypeDetailGradeCriteria criteria)
    {
        var rawData = await repository.FetchAsync<BetTypeDetailInfo>(criteria);

        var scoreByPeriodCriteria = new GetScoreByPeriodCriteria
        {
            FromDate = criteria.FromDate,
            ToDate = criteria.ToDate,
            CustId = criteria.CustId,
            MatchId = criteria.MatchId,
            Bettypes = string.Join(",", rawData.Select(b => b.BetType.ToString())),
            IsExcludeLeagues = true,
            LeagueGroupIds = criteria.LeagueGroupIds
        };

        var bettypeDetail = await CalculateBetType(rawData, criteria.IsLive, scoreByPeriodCriteria);

        return bettypeDetail;
    }

    public async Task<TraderPerformanceList> GetTraderPerformanceOfGradeByLeague(GetTraderPerformanceOfGradeByLeagueCriteria criteria)
    {
        var rawData = await repository.FetchAsync<TraderPerformanceInfo>(criteria);

        var traderPerformance = CalculateTraderPerformanceList(rawData);

        return traderPerformance;
    }
}