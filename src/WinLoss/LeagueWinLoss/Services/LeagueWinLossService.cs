using System.Diagnostics;
using Fanex.Logging;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
namespace LeagueWinLoss.Services;

using Criteria;
using Entities;
using Fanex.Data.Repository;
using Model;

public interface LeagueWinLossService
{
    Task<List<SiteGroup>> GetSiteFilter();

    Task<List<SportGroup>> GetSports();

    Task<LeagueWinLossModel> GetLeagueWinLoss(LeagueWinLossCriteria criteria);

    Task<BettypeWinLossModel> GetBetTypes(BetTypesCriteria criteria);

    Task<BettypeWinLossWithChoiceModel> GetBetTypesWithChoices(BetTypesWithChoiceCriteria criteria);

    Task<BetTypeWinLossByStatusModel> GetBetTypesByLiveNonLiveStatus(BetTypesCriteria criteria);

    List<GlobalShowTime> GetGlobalShowTime();

    List<Status> GetStatus();

    int GetPermissionProfileId(string functionKeyValue);

    Task<MatchWinLossModel> GetMatches(MatchesCriteria criteria);

    Task<MatchWinLossWithChoiceModel> GetMatchesWithChoice(MatchesWithChoiceCriteria criteria);

    Task<MatchWinLossFromBetTypeStatusModel> GetMatchesFromBetTypeStatus(MatchesCriteria criteria);

    Task<SiteWinLossModel> GetSites(SitesCriteria criteria);

    Task<SuperWinLossModel> GetSupers(SuperCriteria criteria);

    Task<List<VirtualRateModel>> GetVirtualRate(VirtualRateCriteria criteria);

    Task<LeagueSuggestionViewModel> GetLeagueSuggestion(LeagueSuggestionCriteria criteria);

    Task<IEnumerable<MarginModel>> GetMargins(MarginCriteria criteria);

    Task<IEnumerable<MarginModel>> RunCachedMargins(MarginCriteria criteria);
}

public class LeagueWinLossServiceImp(
    IDynamicRepository repository,
    SortingService sortingService) : LeagueWinLossService
{
    public async Task<LeagueWinLossModel> GetLeagueWinLoss(LeagueWinLossCriteria criteria)
    {
        var rawData = (await repository.FetchAsync<LeagueWinLossEntity>(criteria))?.ToList() ?? [];
        var leagueWinLoss = new LeagueWinLossModel
        {
            SportWinLoss = [],
            SoccerBARM = new LeagueWinLossItem(),
            OtherSportBARM = new LeagueWinLossItem(),
            SoccerRM = new LeagueWinLossItem(),
            OtherSportRM = new LeagueWinLossItem(),
            Total = new LeagueWinLossItem(),
            GrandTotal = new LeagueWinLossItem()
        };

        var sportGroups = rawData.Where(x => x.LeagueName != "Soccer 8RM"
                                             && x.LeagueName != "Other Sports 8RM"
                                             && x.LeagueName != "Soccer BARM"
                                             && x.LeagueName != "Other Sports BARM"
                                             && x.LeagueName != "SABA Soccer 8RM"
                                             && x.LeagueName != "SABA Soccer BARM"
                                             && x.LeagueName != "SABA Soccer PinGoal 8RM"
                                             && x.LeagueName != "SABA Soccer PinGoal BARM")
            .GroupBy(x => new { x.SportType, x.Sport, x.IsSabaSport });

        foreach (var sportGroup in sportGroups)
        {
            var rawSports = rawData.Where(x => x.SportType == sportGroup.Key.SportType
                                               && x.IsSabaSport == sportGroup.Key.IsSabaSport && sportGroup.Key.Sport == x.Sport);
            var sportName = sportGroup.Key.Sport;
            var leagues = BindingLeagues(rawSports);
            var sportTotal = CalculateSportTotal(sportName, rawSports);

            var sportWinLoss = new SportWinLoss
            {
                SportName = sportName,
                Leagues = leagues.ToList(),
                Total = sportTotal
            };

            leagueWinLoss.SportWinLoss.Add(sportWinLoss);
        }

        var soccerRM = rawData.FirstOrDefault(x => x.LeagueName == "Soccer 8RM");
        leagueWinLoss.SoccerRM = BindingLeague(soccerRM);

        var sabaSoccerRM = rawData.FirstOrDefault(x => x.LeagueName == "SABA Soccer 8RM");
        leagueWinLoss.SabaSoccerRM = BindingLeague(sabaSoccerRM);

        var sabaPinGoalRM = rawData.FirstOrDefault(x => x.LeagueName == "SABA Soccer PinGoal 8RM");
        leagueWinLoss.SabaPinGoalRM = BindingLeague(sabaPinGoalRM);

        var otherRM = rawData.FirstOrDefault(x => x.LeagueName == "Other Sports 8RM");
        leagueWinLoss.OtherSportRM = BindingLeague(otherRM);

        var soccerBARM = rawData.FirstOrDefault(x => x.LeagueName == "Soccer BARM");
        leagueWinLoss.SoccerBARM = BindingLeague(soccerBARM);

        var sabaSoccerBARM = rawData.FirstOrDefault(x => x.LeagueName == "SABA Soccer BARM");
        leagueWinLoss.SabaSoccerBARM = BindingLeague(sabaSoccerBARM);


        var sabaPinGoalBARM = rawData.FirstOrDefault(x => x.LeagueName == "SABA Soccer PinGoal BARM");
        leagueWinLoss.SabaPinGoalBARM = BindingLeague(sabaPinGoalBARM);

        var otherBARM = rawData.FirstOrDefault(x => x.LeagueName == "Other Sports BARM");
        leagueWinLoss.OtherSportBARM = BindingLeague(otherBARM);

        var total = rawData.Where(x => x.LeagueName != "Soccer 8RM"
                                       && x.LeagueName != "SABA Soccer 8RM"
                                       && x.LeagueName != "SABA Soccer PinGoal 8RM"
                                       && x.LeagueName != "Other Sports 8RM"
                                       && x.LeagueName != "Soccer BARM"
                                       && x.LeagueName != "SABA Soccer BARM"
                                       && x.LeagueName != "SABA Soccer PinGoal BARM"
                                       && x.LeagueName != "Other Sports BARM");
        var allTotal = CalculateSportTotal("Total", total);
        leagueWinLoss.Total = allTotal;

        var grandTotal = CalculateSportTotal("Grand Total", rawData);
        leagueWinLoss.GrandTotal = grandTotal;

        if (!string.IsNullOrEmpty(criteria.SortColumnName) && !string.IsNullOrEmpty(criteria.SortDirection))
        {
            leagueWinLoss = sortingService.SortData(leagueWinLoss, criteria.SortDirection, criteria.SortColumnName);
        }

        return leagueWinLoss;
    }

    public async Task<List<SportGroup>> GetSports()
    {
        var sportGroups = new List<SportGroup>();
        var criteria = new SportCriteria();
        var rawData = (await repository.FetchAsync<SportEntity>(criteria)).ToList();

        var groups = rawData.GroupBy(x => new { x.SportGroupType, x.SportGroupName });
        foreach (var group in groups)
        {
            var sportGroup = new SportGroup
            {
                GroupId = group.Key.SportGroupType,
                GroupName = group.Key.SportGroupName,
                ChildItems = []
            };

            var sports = group.ToList().GroupBy(x => new { x.SportType, x.SportName });

            foreach (var sport in sports)
            {
                var sportItem = new Sport
                {
                    SportType = sport.Key.SportType,
                    SportName = sport.Key.SportName,
                    BettypeGroups = []
                };

                var sportBettypes = rawData.Where(x => x.SportType == sport.Key.SportType
                                                       && x.SportName == sport.Key.SportName);

                var groupBettypes = sportBettypes.GroupBy(x => x.BetTypeGroupName);

                foreach (var groupBettype in groupBettypes)
                {
                    var bettypeGroup = new BetTypeGroup
                    {
                        BetTypeGroupName = groupBettype.Key,
                        BetTypes = []
                    };

                    var bettypes = sportBettypes.Where(x => x.BetTypeGroupName == groupBettype.Key);

                    foreach (var bettype in bettypes)
                    {
                        bettypeGroup.BetTypes.Add(new BetType
                        {
                            Bettype = bettype.Bettype,
                            BettypeName = bettype.BettypeName
                        });
                    }

                    sportItem.BettypeGroups.Add(bettypeGroup);
                }

                sportGroup.ChildItems.Add(sportItem);
            }

            sportGroups.Add(sportGroup);
        }

        return sportGroups;
    }

    public async Task<List<SiteGroup>> GetSiteFilter()
    {
        var result = new List<SiteGroup>();
        var criteria = new SiteFilterCriteria();

        var rawData = (await repository.FetchAsync<SiteFilterEntity>(criteria)).ToList();
        var groups = rawData.GroupBy(x => x.SiteGroup);

        foreach (var group in groups)
        {
            var siteGroup = new SiteGroup
            {
                GroupName = group.Key,
                Sites = []
            };
            var sites = rawData.Where(x => x.SiteGroup == group.Key);

            foreach (var site in sites)
            {
                siteGroup.Sites.Add(new Site
                {
                    SiteGroup = site.SiteGroup,
                    SiteName = site.SiteName,
                    SiteLabel = site.SiteLabel
                });
            }

            result.Add(siteGroup);
        }

        return result;
    }

    public async Task<BettypeWinLossModel> GetBetTypes(BetTypesCriteria criteria)
    {
        var result = new BettypeWinLossModel
        {
            BettypeWinLoss = [],
            RM = new BettypeWinLossBySite(),
            BARM = new BettypeWinLossBySite(),
            Total = new BettypeWinLossBySite(),
            GrandTotal = new BettypeWinLossBySite()
        };

        var rawData = (await repository.FetchAsync<BettypeWinLossEntity>(criteria)).ToList();

        if (!rawData.Any())
        {
            return result;
        }

        var rawBettypes = rawData.Where(x => !x.SiteType.Contains(criteria.SportName + " " + "8RM")
                                             && !x.SiteType.Contains(criteria.SportName + " " + "BARM")
                                             && !x.SiteType.Contains("Other Sports 8RM")
                                             && !x.SiteType.Contains("Other Sports BARM"));

        var betTypeGroups = rawBettypes.GroupBy(x => new { x.Bettype, x.BettypeName });
        foreach (var group in betTypeGroups)
        {
            var bettypes = rawBettypes.Where(x => x.Bettype == group.Key.Bettype && x.BettypeName == group.Key.BettypeName);
            var betTypesBySite = BindingBettypes(bettypes);
            var betTypeItem = new BetTypeWinLossItem
            {
                Total = CalculateBettypeTotal(group.Key.BettypeName, bettypes),
                BetTypes = betTypesBySite.ToList()
            };

            result.BettypeWinLoss.Add(betTypeItem);
        }

        result.Total = CalculateBettypeTotal("Total", rawBettypes);

        var eightRM = rawData.FirstOrDefault(x => x.SiteType.Contains("8RM"));
        result.RM = BindingBettype(eightRM);

        var barRM = rawData.FirstOrDefault(x => x.SiteType.Contains("BARM"));
        result.BARM = BindingBettype(barRM);

        result.GrandTotal = CalculateBettypeTotal("Grand Total", rawData);

        return result;
    }

    public async Task<BettypeWinLossWithChoiceModel> GetBetTypesWithChoices(BetTypesWithChoiceCriteria criteria)
    {
        var result = new BettypeWinLossWithChoiceModel
        {
            BettypeWinLoss = [],
            RM = new BettypeWinLossBySite(),
            BARM = new BettypeWinLossBySite(),
            Total = new BettypeWinLossBySite(),
            GrandTotal = new BettypeWinLossBySite()
        };

        var rawData = (await repository.FetchAsync<BettypeWinLossEntity>(criteria)).ToList();

        if (!rawData.Any())
        {
            return result;
        }

        var rawBettypes = rawData.Where(x => !x.SiteType.Contains(criteria.SportName + " " + "8RM")
                                             && !x.SiteType.Contains(criteria.SportName + " " + "BARM")
                                             && !x.SiteType.Contains("Other Sports 8RM")
                                             && !x.SiteType.Contains("Other Sports BARM"));

        var betTypeGroups = rawBettypes.GroupBy(x => new { x.Bettype, x.BettypeName });
        foreach (var group in betTypeGroups)
        {
            var bettypes = rawBettypes.Where(x => x.Bettype == group.Key.Bettype && x.BettypeName == group.Key.BettypeName);

            var betTypeItem = new BetTypeWinLossItemByChoice
            {
                Total = CalculateBettypeTotal(group.Key.BettypeName, bettypes),
                SiteTypes = []
            };
            var betTypeGroupBySiteType = group.GroupBy(x => new { x.SiteType });
            foreach (var siteTypeGroup in betTypeGroupBySiteType)
            {
                var siteTypes = bettypes.Where(x => x.SiteType == siteTypeGroup.Key.SiteType);
                var siteTypeItem = new BetTypeWinLossSiteTypeItem
                {
                    Total = CalculateBettypeTotal(siteTypeGroup.Key.SiteType, siteTypes),
                    Choices = BindingBettypes(siteTypes.Where(x => !string.IsNullOrEmpty(x.Choice))).ToList()
                };
                betTypeItem.SiteTypes.Add(siteTypeItem);
            }

            result.BettypeWinLoss.Add(betTypeItem);
        }

        result.Total = CalculateBettypeTotal("Total", rawBettypes);

        var eightRM = rawData.FirstOrDefault(x => x.SiteType.Contains("8RM"));
        result.RM = BindingBettype(eightRM);

        var barRM = rawData.FirstOrDefault(x => x.SiteType.Contains("BARM"));
        result.BARM = BindingBettype(barRM);

        result.GrandTotal = CalculateBettypeTotal("Grand Total", rawData);

        return result;
    }

    public async Task<BetTypeWinLossByStatusModel> GetBetTypesByLiveNonLiveStatus(BetTypesCriteria criteria)
    {
        var result = new BetTypeWinLossByStatusModel();
        var liveIndicator = criteria.LiveIndicator;
        if (liveIndicator is 1 or 2)
        {
            criteria.LiveIndicator = 1;
            var liveBetTypes = await GetBetTypesLive(criteria);
            result.BetTypeWinLossLive = liveBetTypes;
        }

        if (liveIndicator != 0 && liveIndicator != 2)
        {
            return result;
        }

        criteria.LiveIndicator = 0;
        var nonLiveBetTypes = await GetBetTypesNonLive(criteria);
        result.BetTypeWinLossNonLive = nonLiveBetTypes;

        return result;
    }

    public List<GlobalShowTime> GetGlobalShowTime()
    {
        var result = new List<GlobalShowTime>
        {
            new() { Value = 0, Time = "00:00 - 23:59"},
            new() { Value = 1, Time = "12:00 - 12:59"},
            new() { Value = 2, Time = "13:00 - 13:59"},
            new() { Value = 3, Time = "14:00 - 15:59"},
            new() { Value = 4, Time = "15:00 - 15:59"},
            new() { Value = 5, Time = "16:00 - 16:59"},
            new() { Value = 6, Time = "17:00 - 17:59"},
            new() { Value = 7, Time = "18:00 - 18:59"},
            new() { Value = 8, Time = "19:00 - 19:59"},
            new() { Value = 9, Time = "20:00 - 20:59"},
            new() { Value = 10, Time = "21:00 - 21:59"},
            new() { Value = 11, Time = "22:00 - 22:59"},
            new() { Value = 12, Time = "23:00 - 23:59"},
            new() { Value = 13, Time = "00:00 - 00:59"},
            new() { Value = 14, Time = "01:00 - 01:59"},
            new() { Value = 15, Time = "02:00 - 02:59"},
            new() { Value = 16, Time = "03:00 - 03:59"},
            new() { Value = 17, Time = "04:00 - 04:59"},
            new() { Value = 18, Time = "05:00 - 05:59"},
            new() { Value = 19, Time = "06:00 - 06:59"},
            new() { Value = 20, Time = "07:00 - 07:59"},
            new() { Value = 21, Time = "08:00 - 08:59"},
            new() { Value = 22, Time = "09:00 - 09:59"},
            new() { Value = 23, Time = "10:00 - 10:59"},
            new() { Value = 24, Time = "11:00 - 11:59"}
        };

        return result;
    }

    public List<Status> GetStatus()
    {
        return
        [
            new Status { Name = "All", Value = 2 },
            new Status { Name = "Live", Value = 1 },
            new Status { Name = "Non-Live", Value = 0 }
        ];
    }

    public int GetPermissionProfileId(string functionKeyValue)
    {
        var criteria = new PermissionCriteria
        {
            FunctionKey = functionKeyValue
        };

        var dataSet = repository.FetchDataSet(criteria);

        if (dataSet == null || dataSet.Tables.Count <= 0 || dataSet.Tables[0].Rows.Count <= 0)
        {
            return 0;
        }

        var firstRow = dataSet.Tables[0].Rows[0];
        var permissionProfileId = Convert.ToInt32(firstRow["PermisionProfileID"]);

        return permissionProfileId;

    }

    private async Task<BetTypeWinLossLiveModel> GetBetTypesLive(BetTypesCriteria criteria)
    {
        const int maxValueOfPenaltyBettype = -1000;
        var result = new BetTypeWinLossLiveModel
        {
            BetTypeWinLossLiveGroups = [],
            Total = new BettypeWinLossBySite(),
            RM = new BettypeWinLossBySite(),
            BARM = new BettypeWinLossBySite(),
            GrandTotal = new BettypeWinLossBySite()
        };

        var rawData = (await repository.FetchAsync<BettypeWinLossEntity>(criteria)).ToList();

        if (!rawData.Any())
        {
            return result;
        }

        var rawBettypes = rawData.Where(x => !x.SiteType.Contains(criteria.SportName + " " + "8RM")
                                             && !x.SiteType.Contains(criteria.SportName + " " + "BARM")
                                             && !x.SiteType.Contains("Other Sports 8RM")
                                             && !x.SiteType.Contains("Other Sports BARM"));

        var rawBetTypesWithOutPenalty = rawBettypes.Where(x => x.Bettype > maxValueOfPenaltyBettype);

        var betTypesWithOutPenalty = new BetTypeWinLossLiveGroupModel
        {
            BettypeWinLoss = [],
            Total = new BettypeWinLossBySite()
        };

        betTypesWithOutPenalty.BettypeWinLoss = BindingBettypes(rawBetTypesWithOutPenalty).ToList();
        betTypesWithOutPenalty.Total = CalculateBettypeTotal("Main Total", rawBetTypesWithOutPenalty);
        result.BetTypeWinLossLiveGroups.Add(betTypesWithOutPenalty);

        var rawPenaltyBetTypes = rawBettypes.Where(x => x.Bettype <= maxValueOfPenaltyBettype);
        var penaltyBetTypes = new BetTypeWinLossLiveGroupModel
        {
            BettypeWinLoss = [],
            Total = new BettypeWinLossBySite()
        };

        penaltyBetTypes.BettypeWinLoss = BindingBettypes(rawPenaltyBetTypes).ToList();
        penaltyBetTypes.Total = CalculateBettypeTotal("Pen Total", rawPenaltyBetTypes);
        result.BetTypeWinLossLiveGroups.Add(penaltyBetTypes);

        var total = CalculateBettypeTotal("Total", rawBettypes);
        result.Total = total;

        var eightRM = rawData.FirstOrDefault(x => x.SiteType.Contains("8RM"));
        result.RM = BindingBettype(eightRM);

        var barRM = rawData.FirstOrDefault(x => x.SiteType.Contains("BARM"));
        result.BARM = BindingBettype(barRM);

        result.GrandTotal = CalculateBettypeTotal("Grand Total", rawData);


        return result;
    }

    private async Task<BetTypeWinLossNonLiveModel> GetBetTypesNonLive(BetTypesCriteria criteria)
    {
        var result = new BetTypeWinLossNonLiveModel
        {
            BettypeWinLoss = [],
            RM = new BettypeWinLossBySite(),
            BARM = new BettypeWinLossBySite(),
            Total = new BettypeWinLossBySite(),
            GrandTotal = new BettypeWinLossBySite()
        };

        var rawData = (await repository.FetchAsync<BettypeWinLossEntity>(criteria)).ToList();

        if (!rawData.Any())
        {
            return result;
        }

        var rawBettypes = rawData.Where(x => !x.SiteType.Contains(criteria.SportName + " " + "8RM")
                                             && !x.SiteType.Contains(criteria.SportName + " " + "BARM")
                                             && !x.SiteType.Contains("Other Sports 8RM")
                                             && !x.SiteType.Contains("Other Sports BARM"));

        result.BettypeWinLoss = BindingBettypes(rawBettypes).ToList();

        result.Total = CalculateBettypeTotal("Total", rawBettypes);

        var eightRM = rawData.FirstOrDefault(x => x.SiteType.Contains("8RM"));
        result.RM = BindingBettype(eightRM);

        var barRM = rawData.FirstOrDefault(x => x.SiteType.Contains("BARM"));
        result.BARM = BindingBettype(barRM);

        result.GrandTotal = CalculateBettypeTotal("Grand Total", rawData);

        return result;
    }

    public async Task<MatchWinLossModel> GetMatches(MatchesCriteria criteria)
    {
        await LogService.InfoAsync($"param: {JsonConvert.SerializeObject(criteria)}");
        var sw = new Stopwatch();
        sw.Start();
        var rawData = (await repository.FetchAsync<MatchEntity>(criteria)).ToList();
        sw.Stop();
        await LogService.InfoAsync($"sp(miliseconds) {sw.ElapsedMilliseconds}");
        sw.Reset();
        await LogService.InfoAsync("web: start");
        sw.Start();
        var leagueWinLoss = new MatchWinLossModel
        {
            Match = [],
            RM = new MatchWinLossByLiveStatus(),
            BARM = new MatchWinLossByLiveStatus(),
            Total = new MatchWinLossByLiveStatus(),
            GrandTotal = new MatchWinLossByLiveStatus()
        };

        var rawMatches = rawData.Where(x =>
            !x.MatchEvent.Contains(criteria.SportName + " " + "BARM")
            && !x.MatchEvent.Contains(criteria.SportName + " " + "8RM")
            && !x.MatchEvent.Contains("Other Sports 8RM")
            && !x.MatchEvent.Contains("Other Sports BARM"));

        var matchGroups = rawMatches.GroupBy(x => new { x.MatchID, x.MatchEvent });
        foreach (var group in matchGroups)
        {
            var matches = rawMatches.Where(x => x.MatchID == group.Key.MatchID && x.MatchEvent == group.Key.MatchEvent);
            var matchesByLiveStatus = BindingMatches(matches);
            var matchWinLossItem = new MatchWinLossItem
            {
                Total = CalculateMatchTotal(group.Key.MatchEvent, matches),
                Matches = matchesByLiveStatus.ToList()
            };

            leagueWinLoss.Match.Add(matchWinLossItem);
        }

        leagueWinLoss.Total = CalculateMatchTotal("Total", rawMatches);

        var eightRM = rawData.FirstOrDefault(x => x.MatchEvent.Contains("8RM"));
        leagueWinLoss.RM = BindingMatch(eightRM);

        var barRM = rawData.FirstOrDefault(x => x.MatchEvent.Contains("BARM"));
        leagueWinLoss.BARM = BindingMatch(barRM);

        leagueWinLoss.GrandTotal = CalculateMatchTotal("Grand Total", rawData);
        sw.Stop();
        await LogService.InfoAsync($"web (miliseconds) {sw.ElapsedMilliseconds}");
        return leagueWinLoss;
    }

    public async Task<MatchWinLossWithChoiceModel> GetMatchesWithChoice(MatchesWithChoiceCriteria criteria)
    {
        var rawData = (await repository.FetchAsync<MatchEntity>(criteria)).ToList();

        var leagueWinLoss = new MatchWinLossWithChoiceModel
        {
            Match = [],
            RM = new MatchWinLossByLiveStatus(),
            BARM = new MatchWinLossByLiveStatus(),
            Total = new MatchWinLossByLiveStatus(),
            GrandTotal = new MatchWinLossByLiveStatus()
        };

        var rawMatches = rawData.Where(x =>
            !x.MatchEvent.Contains(criteria.SportName + " " + "BARM")
            && !x.MatchEvent.Contains(criteria.SportName + " " + "8RM")
            && !x.MatchEvent.Contains("Other Sports 8RM")
            && !x.MatchEvent.Contains("Other Sports BARM"));

        var matchGroups = rawMatches.GroupBy(x => new { x.MatchID, x.MatchEvent });
        foreach (var group in matchGroups)
        {
            var matches = rawMatches.Where(x => x.MatchID == group.Key.MatchID && x.MatchEvent == group.Key.MatchEvent);
            var matchWinLossItem = new MatchWinLossItemByChoice
            {
                Total = CalculateMatchTotal(group.Key.MatchEvent, matches),
                LiveIndicators = []
            };
            var matchGroupByLiveIndicator = group.GroupBy(x => new { x.LiveIndicator });
            foreach (var liveIndicatorGroup in matchGroupByLiveIndicator)
            {
                var liveIndicators = matches.Where(x => x.LiveIndicator == liveIndicatorGroup.Key.LiveIndicator);
                var liveIndicatorItem = new MatchWinLossLiveIndicatorItem
                {
                    Total = CalculateMatchTotal(liveIndicatorGroup.Key.LiveIndicator ? "Live" : "Non-Live", liveIndicators),
                    BetTypes = []
                };
                var matchGroupByBetType = liveIndicatorGroup.Where(x => x.BetType != 0).GroupBy(x => new { x.BetTypeName });
                foreach (var betTypeGroup in matchGroupByBetType)
                {
                    var betTypes = betTypeGroup.Where(x => x.BetTypeName == betTypeGroup.Key.BetTypeName);
                    var betTypeItem = new MatchWinLossLiveBetTypeItem
                    {
                        Total = CalculateMatchTotal(betTypeGroup.Key.BetTypeName, betTypes),
                        Choices = BindingMatches(betTypes.Where(x => !string.IsNullOrEmpty(x.Choice))).ToList()
                    };
                    liveIndicatorItem.BetTypes.Add(betTypeItem);
                }
                matchWinLossItem.LiveIndicators.Add(liveIndicatorItem);
            }

            leagueWinLoss.Match.Add(matchWinLossItem);
        }

        leagueWinLoss.Total = CalculateMatchTotal("Total", rawMatches);

        var eightRM = rawData.FirstOrDefault(x => x.MatchEvent.Contains("8RM"));
        leagueWinLoss.RM = BindingMatch(eightRM);

        var barRM = rawData.FirstOrDefault(x => x.MatchEvent.Contains("BARM"));
        leagueWinLoss.BARM = BindingMatch(barRM);

        leagueWinLoss.GrandTotal = CalculateMatchTotal("Grand Total", rawData);
        return leagueWinLoss;
    }

    public async Task<MatchWinLossFromBetTypeStatusModel> GetMatchesFromBetTypeStatus(MatchesCriteria criteria)
    {
        var rawData = (await repository.FetchAsync<MatchEntity>(criteria)).ToList();
        var leagueWinLoss = new MatchWinLossFromBetTypeStatusModel
        {
            Match = [],
            RM = new MatchWinLossByLiveStatus(),
            BARM = new MatchWinLossByLiveStatus(),
            Total = new MatchWinLossByLiveStatus(),
            GrandTotal = new MatchWinLossByLiveStatus()
        };

        var rawMatches = rawData.Where(x =>
            !x.MatchEvent.Contains(criteria.SportName + " " + "BARM")
            && !x.MatchEvent.Contains(criteria.SportName + " " + "8RM")
            && !x.MatchEvent.Contains("Other Sports 8RM")
            && !x.MatchEvent.Contains("Other Sports BARM"));
        leagueWinLoss.Match = BindingMatches(rawMatches).ToList();

        leagueWinLoss.Total = CalculateMatchTotal("Total", rawMatches);
        var eightRM = rawData.FirstOrDefault(x => x.MatchEvent.Contains("8RM"));
        leagueWinLoss.RM = BindingMatch(eightRM);

        var barRM = rawData.FirstOrDefault(x => x.MatchEvent.Contains("BARM"));
        leagueWinLoss.BARM = BindingMatch(barRM);

        leagueWinLoss.GrandTotal = CalculateMatchTotal("Grand Total", rawData);

        return leagueWinLoss;
    }

    public async Task<SiteWinLossModel> GetSites(SitesCriteria criteria)
    {
        var rawData = (await repository.FetchAsync<SiteEntity>(criteria)).ToList();
        var leagueWinLoss = new SiteWinLossModel
        {
            Sites = [],
            RM = new SiteWinLossItem(),
            BARM = new SiteWinLossItem(),
            Total = new SiteWinLossItem(),
            GrandTotal = new SiteWinLossItem()
        };

        var rawSites = rawData.Where(x =>
            !x.SiteName.Contains(criteria.SportName + " " + "BARM")
            && !x.SiteName.Contains(criteria.SportName + " " + "8RM")
            && !x.SiteName.Contains("Other Sports 8RM")
            && !x.SiteName.Contains("Other Sports BARM"));

        var sites = BindingSites(rawSites);
        leagueWinLoss.Sites = sites.ToList();

        leagueWinLoss.Total = CalculateSiteTotal("Total", rawSites);

        var eightRM = rawData.FirstOrDefault(x => x.SiteName.Contains("8RM"));
        leagueWinLoss.RM = BindingSite(eightRM);

        var barRM = rawData.FirstOrDefault(x => x.SiteName.Contains("BARM"));
        leagueWinLoss.BARM = BindingSite(barRM);

        leagueWinLoss.GrandTotal = CalculateSiteTotal("Grand Total", rawData);

        if (!string.IsNullOrEmpty(criteria.SortColumnName) && !string.IsNullOrEmpty(criteria.SortDirection))
        {
            leagueWinLoss = sortingService.SortSiteData(leagueWinLoss, criteria.SortDirection, criteria.SortColumnName);
        }

        return leagueWinLoss;
    }

    public async Task<SuperWinLossModel> GetSupers(SuperCriteria criteria)
    {
        var rawData = (await repository.FetchAsync<SuperEntity>(criteria)).ToList();
        var leagueWinLoss = new SuperWinLossModel
        {
            Supers = [],
            RM = new SuperWinLossItem(),
            BARM = new SuperWinLossItem(),
            Total = new SuperWinLossItem(),
            GrandTotal = new SuperWinLossItem()
        };

        var rawSupers = rawData.Where(x =>
            !x.SuperName.Contains(criteria.SportName + " " + "BARM")
            && !x.SuperName.Contains(criteria.SportName + " " + "8RM")
            && !x.SuperName.Contains("Other Sports 8RM")
            && !x.SuperName.Contains("Other Sports BARM"));

        var supers = BindingSupers(rawSupers);
        leagueWinLoss.Supers = supers.ToList();

        leagueWinLoss.Total = CalculateSuperTotal("Total", rawSupers);

        var eightRM = rawData.FirstOrDefault(x => x.SuperName.Contains("8RM"));
        leagueWinLoss.RM = BindingSuper(eightRM);

        var barRM = rawData.FirstOrDefault(x => x.SuperName.Contains("BARM"));
        leagueWinLoss.BARM = BindingSuper(barRM);

        leagueWinLoss.GrandTotal = CalculateSuperTotal("Grand Total", rawData);

        return leagueWinLoss;
    }

    public async Task<List<VirtualRateModel>> GetVirtualRate(VirtualRateCriteria criteria)
    {
        var rawData = (await repository.FetchAsync<VirtualRateEntity>(criteria)).ToList();

        return rawData.Select(item => new VirtualRateModel { Sport = item.Sport, BetType = item.BetType, LiveStatus = item.LiveStatus, VirtualRate = item.VirtualRate }).ToList();
    }

    public async Task<LeagueSuggestionViewModel> GetLeagueSuggestion(LeagueSuggestionCriteria criteria)
    {
        var leagues = await repository.FetchAsync<string>(criteria);
        var suggestionResult = new LeagueSuggestionViewModel
        {
            Query = criteria.SearchText,
            Suggestions = [],
            DataInfo = [string.Empty]
        };
        leagues.ToList().ForEach(x => suggestionResult.Suggestions.Add(x));
        return suggestionResult;
    }

    public Task<IEnumerable<MarginModel>> RunCachedMargins(MarginCriteria criteria)
    {
        return repository.FetchAsync<MarginModel>(criteria);
    }

    public async Task<IEnumerable<MarginModel>> GetMargins(MarginCriteria criteria)
    {
        var result = new List<MarginModel>();
        var months = GetMonthsFromCriteria(criteria.FromDate, criteria.ToDate);
        var margins = await repository.FetchAsync<MarginModel>(criteria);
        foreach (var month in months)
        {
            var margin = margins.FirstOrDefault(item => item.Date == month);
            if (margin == null)
            {
                result.Add(new MarginModel
                {
                    MonthYear = $"{month.Substring(4, 2)}-{month[..4]}",
                    CompanyMargin = 0
                });
            }
            else
            {
                result.Add(new MarginModel
                {
                    MonthYear = $"{month.Substring(4, 2)}-{month[..4]}",
                    CompanyMargin = margin.CompanyMargin
                });
            }
        }

        return result;
    }

    private List<string> GetMonthsFromCriteria(DateTime fromDate, DateTime toDate)
    {
        var months = new List<string>();
        var currentDate = fromDate;

        while (currentDate <= toDate)
        {
            months.Add(currentDate.ToString("yyyyMM"));
            currentDate = currentDate.AddMonths(1);
        }

        return months;
    }


    private LeagueWinLossItem CalculateSportTotal(string sport, IEnumerable<LeagueWinLossEntity> sportGroup)
    {
        return new LeagueWinLossItem
        {
            LeagueName = sport,
            MemberWinLoss = sportGroup.Sum(x => x.MemberWinLoss),
            MatchCount = sportGroup.Sum(x => x.MatchCount),
            SMAWinLoss = sportGroup.Sum(x => x.SMAWinLoss),
            ExtraPT = sportGroup.Sum(x => x.ExtraPT),
            Commission = sportGroup.Sum(x => x.Commission),
            VirtualWinLoss = sportGroup.Sum(x => x.VirtualWinLoss),
            RealIncome = sportGroup.Sum(x => x.RealIncome),
            VirtualTurnover = sportGroup.Sum(x => x.VirtualTurnover),
            RealTurnover = sportGroup.Sum(x => x.RealTurnover)
        };
    }

    private IEnumerable<LeagueWinLossItem> BindingLeagues(IEnumerable<LeagueWinLossEntity> sportGroup)
    {
        return sportGroup.Select(BindingLeague);
    }

    private LeagueWinLossItem BindingLeague(LeagueWinLossEntity rawData)
    {
        if (rawData == null)
        {
            return new LeagueWinLossItem();
        }

        return new LeagueWinLossItem
        {
            Sport = rawData.Sport,
            SportType = rawData.SportType,
            LeagueId = rawData.LeagueId,
            LeagueName = rawData.LeagueName,
            MemberWinLoss = rawData.MemberWinLoss,
            SMAWinLoss = rawData.SMAWinLoss,
            ExtraPT = rawData.ExtraPT,
            Commission = rawData.Commission,
            VirtualWinLoss = rawData.VirtualWinLoss,
            RealIncome = rawData.RealIncome,
            VirtualTurnover = rawData.VirtualTurnover,
            RealTurnover = rawData.RealTurnover,
            MatchCount = rawData.MatchCount
        };
    }

    private BettypeWinLossBySite CalculateBettypeTotal(string bettype, IEnumerable<BettypeWinLossEntity> bettypes)
    {
        return new BettypeWinLossBySite
        {
            BettypeName = bettype,
            MemberWinLoss = bettypes.Sum(x => x.MemberWinLoss),
            SMAWinLoss = bettypes.Sum(x => x.SMAWinLoss),
            ExtraPT = bettypes.Sum(x => x.ExtraPT),
            Commission = bettypes.Sum(x => x.Commission),
            VirtualWinLoss = bettypes.Sum(x => x.VirtualWinLoss),
            RealIncome = bettypes.Sum(x => x.RealIncome),
            VirtualTurnover = bettypes.Sum(x => x.VirtualTurnover),
            RealTurnover = bettypes.Sum(x => x.RealTurnover),
            TraderName = bettypes.FirstOrDefault()?.TraderName,
            Bettype = (bettypes.FirstOrDefault() ?? new BettypeWinLossEntity()).Bettype
        };
    }

    private IEnumerable<BettypeWinLossBySite> BindingBettypes(IEnumerable<BettypeWinLossEntity> bettypeGroup)
    {
        return bettypeGroup.Select(BindingBettype);
    }

    private BettypeWinLossBySite BindingBettype(BettypeWinLossEntity rawData)
    {
        if (rawData == null)
        {
            return new BettypeWinLossBySite();
        }

        return new BettypeWinLossBySite
        {
            Bettype = rawData.Bettype,
            BettypeName = rawData.BettypeName,
            SiteType = rawData.SiteType,
            MemberWinLoss = rawData.MemberWinLoss,
            SMAWinLoss = rawData.SMAWinLoss,
            ExtraPT = rawData.ExtraPT,
            Commission = rawData.Commission,
            VirtualWinLoss = rawData.VirtualWinLoss,
            RealIncome = rawData.RealIncome,
            VirtualTurnover = rawData.VirtualTurnover,
            RealTurnover = rawData.RealTurnover,
            IsHedge = rawData.IsHedge,
            Choice = rawData.Choice
        };
    }

    private MatchWinLossByLiveStatus CalculateMatchTotal(string title, IEnumerable<MatchEntity> matches)
    {
        decimal memberWinLoss = 0,
            smaWinLoss = 0,
            extraPT = 0,
            commission = 0,
            virtualWinLoss = 0,
            realIncome = 0,
            virtualTurnover = 0,
            realTurnover = 0;

        foreach (var match in matches)
        {
            memberWinLoss += match.MemberWinLoss;
            smaWinLoss += match.SMAWinLoss;
            extraPT += match.ExtraPT;
            commission += match.Commission;
            virtualWinLoss += match.VirtualWinLoss;
            realIncome += match.RealIncome;
            virtualTurnover += match.VirtualTurnover;
            realTurnover += match.RealTurnover;
        }

        var firstMatch = matches.FirstOrDefault() ?? new MatchEntity();

        return new MatchWinLossByLiveStatus
        {
            MatchEvent = title,
            MatchId = firstMatch.MatchID,
            MatchCode = firstMatch.MatchCode,
            MemberWinLoss = memberWinLoss,
            SMAWinLoss = smaWinLoss,
            ExtraPT = extraPT,
            Commission = commission,
            VirtualWinLoss = virtualWinLoss,
            RealIncome = realIncome,
            VirtualTurnover = virtualTurnover,
            RealTurnover = realTurnover,
            IsOutright = firstMatch.IsOutright,
            LiveIndicator = firstMatch.LiveIndicator
        };
    }

    private IEnumerable<MatchWinLossByLiveStatus> BindingMatches(IEnumerable<MatchEntity> matches)
    {
        return matches.Select(BindingMatch);
    }

    private MatchWinLossByLiveStatus BindingMatch(MatchEntity rawData)
    {
        if (rawData == null)
        {
            return new MatchWinLossByLiveStatus();
        }

        return new MatchWinLossByLiveStatus
        {
            MatchId = rawData.MatchID,
            LiveIndicator = rawData.LiveIndicator,
            MatchEvent = rawData.MatchEvent,
            MemberWinLoss = rawData.MemberWinLoss,
            SMAWinLoss = rawData.SMAWinLoss,
            ExtraPT = rawData.ExtraPT,
            Commission = rawData.Commission,
            VirtualWinLoss = rawData.VirtualWinLoss,
            RealIncome = rawData.RealIncome,
            VirtualTurnover = rawData.VirtualTurnover,
            RealTurnover = rawData.RealTurnover,
            IsHedge = rawData.IsHedge,
            IsOutright = rawData.IsOutright,
            BetTypeName = rawData.BetTypeName,
            Choice = rawData.Choice
        };
    }

    private SiteWinLossItem CalculateSiteTotal(string title, IEnumerable<SiteEntity> sites)
    {
        return new SiteWinLossItem
        {
            SiteName = title,
            MemberWinLoss = sites.Sum(x => x.MemberWinLoss),
            SMAWinLoss = sites.Sum(x => x.SMAWinLoss),
            ExtraPT = sites.Sum(x => x.ExtraPT),
            Commission = sites.Sum(x => x.Commission),
            VirtualWinLoss = sites.Sum(x => x.VirtualWinLoss),
            RealIncome = sites.Sum(x => x.RealIncome),
            VirtualTurnover = sites.Sum(x => x.VirtualTurnover),
            RealTurnover = sites.Sum(x => x.RealTurnover)
        };
    }

    private IEnumerable<SiteWinLossItem> BindingSites(IEnumerable<SiteEntity> sites)
    {
        return sites.Select(BindingSite);
    }

    private SiteWinLossItem BindingSite(SiteEntity rawData)
    {
        if (rawData == null)
        {
            return new SiteWinLossItem();
        }

        return new SiteWinLossItem
        {
            SiteId = rawData.SiteID,
            SiteName = rawData.SiteName,
            MemberWinLoss = rawData.MemberWinLoss,
            SMAWinLoss = rawData.SMAWinLoss,
            ExtraPT = rawData.ExtraPT,
            Commission = rawData.Commission,
            VirtualWinLoss = rawData.VirtualWinLoss,
            RealIncome = rawData.RealIncome,
            VirtualTurnover = rawData.VirtualTurnover,
            RealTurnover = rawData.RealTurnover
        };
    }

    private SuperWinLossItem CalculateSuperTotal(string title, IEnumerable<SuperEntity> sites)
    {
        return new SuperWinLossItem
        {
            SuperName = title,
            MemberWinLoss = sites.Sum(x => x.MemberWinLoss),
            SMAWinLoss = sites.Sum(x => x.SMAWinLoss),
            ExtraPT = sites.Sum(x => x.ExtraPT),
            Commission = sites.Sum(x => x.Commission),
            VirtualWinLoss = sites.Sum(x => x.VirtualWinLoss),
            RealIncome = sites.Sum(x => x.RealIncome),
            VirtualTurnover = sites.Sum(x => x.VirtualTurnover),
            RealTurnover = sites.Sum(x => x.RealTurnover)
        };
    }

    private IEnumerable<SuperWinLossItem> BindingSupers(IEnumerable<SuperEntity> sites)
    {
        return sites.Select(BindingSuper);
    }

    private SuperWinLossItem BindingSuper(SuperEntity rawData)
    {
        if (rawData == null)
        {
            return new SuperWinLossItem();
        }

        return new SuperWinLossItem
        {
            SuperId = rawData.SuperID,
            SuperName = rawData.SuperName,
            MemberWinLoss = rawData.MemberWinLoss,
            SMAWinLoss = rawData.SMAWinLoss,
            ExtraPT = rawData.ExtraPT,
            Commission = rawData.Commission,
            VirtualWinLoss = rawData.VirtualWinLoss,
            RealIncome = rawData.RealIncome,
            VirtualTurnover = rawData.VirtualTurnover,
            RealTurnover = rawData.RealTurnover,
            HasVR = rawData.HasVR
        };
    }
}