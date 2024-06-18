using DailyMixParlay.Constant;
using DailyMixParlay.Criteria;
using DailyMixParlay.Models;
using Fanex.Data.Repository;
using Utilities.LinqDynamicSum;
using Utilities.RemotingRef;

namespace DailyMixParlayWinLoss.Services
{
    public interface DailyMixParlayWinLossService
    {
        Task<WinLoss> GetCombinationWinLossReport(GetCombinationWinLossCriteria criteria);
    }

    public class DailyMixParlayWinLossServiceImp : DailyMixParlayWinLossService
    {
        private readonly IDynamicRepository repository;

        private readonly RemotingRefService remotingRefService;

        public DailyMixParlayWinLossServiceImp(IDynamicRepository repository, RemotingRefService remotingRefService)
        {
            this.repository = repository;
            this.remotingRefService = remotingRefService;
        }

        public async Task<WinLoss> GetCombinationWinLossReport(GetCombinationWinLossCriteria criteria)
        {
            var report = new WinLoss();

            var rawData = await repository.FetchAsync<WinLossItem>(criteria);

            if (rawData != null)
            {
                var combinationGroups = rawData.GroupBy(data => data.Combinations);

                var combinationWinLosses = from combinationGroup in combinationGroups
                         let combinationGroupRawData = rawData.Where(x =>x.Combinations == combinationGroup.Key)
                         select CalculateCombinationGroup(combinationGroup.Key, combinationGroupRawData);

                var total = rawData.DynamicSum();
                total.Name = "GrandTotal";

                report.WinLosses = combinationWinLosses.ToList();
                report.Total = total;
               
            }

            return report;
        }

        private WinLossByCombination CalculateCombinationGroup(string combination, IEnumerable<WinLossItem> rawData)
        {
            var combinationName = this.ConvertCombinationType(combination);

            var result = CalculateCombinationByLiveType(rawData);
            result.Name = combinationName;

            return result;
        }

        private WinLossByCombination CalculateCombinationByLiveType(IEnumerable<WinLossItem> rawData)
        {
            var liveTypeGroups = rawData.GroupBy(x => x.LiveType);

            var liveTypeWinLoss = from liveTypeGroup in liveTypeGroups
                      let liveTypeRawData = rawData.Where(x => x.LiveType == liveTypeGroup.Key)
                      select CalculateCombinationByLiveTypeGroup(liveTypeGroup.Key, liveTypeRawData);

            var total = rawData.DynamicSum();
            total.Name = "Total";

            return new WinLossByCombination
            {
                Name = string.Empty,
                WinLosses = liveTypeWinLoss.ToList(),
                Total = total
            };
        }

        private WinLossByLiveStatus CalculateCombinationByLiveTypeGroup(bool liveType, IEnumerable<WinLossItem> rawData)
        {
            var total = rawData.DynamicSum();
            total.Name = "Sub Total";

            var winlosses = from item in rawData
                      select CalculateWinLossItem(item);

            return new WinLossByLiveStatus
            { 
                Name = liveType ? "Live" : "Non-Live",
                WinLosses = winlosses.ToList(),
                Total = total
            };
        }

        private WinLossItem CalculateWinLossItem(WinLossItem item)
        {
            var result = item;
            result.Name = $"Mix Parlay ({item.Status})";
            
            return result;
        }

        public WinLossInfo GetAdvanceMixParlayReport(GetAdvanceMixParlayReportCriteria criteria)
        {
            WinLossInfo report = new WinLossInfo
            {
                GrandTotal = new WinLossInfo
                {
                    Name = Labels.Grand_Total
                },
                ChildsInfo = new List<WinLossInfo>()
            };

            var rawData = this.GetReportData<dynamic>(criteria);
            if (rawData != null)
            {
                var sportTypeList = rawData.Select(data => Converter.ToInt(data.SportType))
                                           .ToList();
                this.GetSportName(sportTypeList, ref this._sportTypeNameCache);

                var liveTypeGroups = rawData.GroupBy(data => data.LiveType);
                foreach (var liveTypeGroup in liveTypeGroups)
                {
                    var liveTypeWinLoss = new WinLossInfo
                    {
                        Id = Converter.ToInt(liveTypeGroup.Key),
                        ChildsInfo = new List<WinLossInfo>(),
                        GrandTotal = new WinLossInfo
                        {
                            Name = Labels.Total
                        }
                    };

                    liveTypeWinLoss.Name = this.ConvertLiveType(liveTypeWinLoss.Id);
                    var sportGroups = liveTypeGroup.GroupBy(data => data.SportType);
                    foreach (var sportGroup in sportGroups)
                    {
                        var sportWinLoss = new WinLossInfo
                        {
                            Id = Converter.ToInt(sportGroup.Key),
                            ChildsInfo = new List<WinLossInfo>(),
                            GrandTotal = new WinLossInfo
                            {
                                Name = Labels.Sub_Total
                            }
                        };

                        sportWinLoss.Name = this._sportTypeNameCache[sportWinLoss.Id];
                        foreach (var data in sportGroup)
                        {
                            WinLossInfo info = new WinLossInfo
                            {
                                Id = criteria.Bettype,
                                Status = Converter.ToString2(data.status),
                                MemberWinLoss = Converter.ToDecimal(data.MemberWinLoss),
                                SMAWinLoss = Converter.ToDecimal(data.SMAWinLoss),
                                Commission = Converter.ToDecimal(data.Commission),
                                RealIncome = Converter.ToDecimal(data.RealIncome),
                                RealTurnover = Converter.ToDecimal(data.RealTurnover),
                                ExtraPT = Converter.ToDecimal(data.ExtraPT),
                                MemberBonus = Converter.ToDecimal(data.MemberBonus),
                                HouseBonus = Converter.ToDecimal(data.HouseBonus)
                            };

                            info.Name = string.Format(Labels.Mix_Parlay + " ({0})", info.Status);
                            info.Margin = this.CalculateMargin(info.RealIncome, info.RealTurnover);
                            sportWinLoss.GrandTotal.Add(info);
                            info = this.FormatWinLossInfo(info);
                            sportWinLoss.ChildsInfo.Add(info);

                            sportWinLoss.TotalRowDetail++;
                            liveTypeWinLoss.TotalRowDetail++;
                        }

                        sportWinLoss.GrandTotal.Margin = this.CalculateMargin(
                           sportWinLoss.GrandTotal.RealIncome,
                           sportWinLoss.GrandTotal.RealTurnover);
                        liveTypeWinLoss.GrandTotal.Add(sportWinLoss.GrandTotal);
                        sportWinLoss.GrandTotal = this.FormatWinLossInfo(sportWinLoss.GrandTotal);
                        liveTypeWinLoss.ChildsInfo.Add(sportWinLoss);
                    }

                    liveTypeWinLoss.GrandTotal.Margin = this.CalculateMargin(
                      liveTypeWinLoss.GrandTotal.RealIncome,
                      liveTypeWinLoss.GrandTotal.RealTurnover);
                    report.GrandTotal.Add(liveTypeWinLoss.GrandTotal);
                    liveTypeWinLoss.GrandTotal = this.FormatWinLossInfo(liveTypeWinLoss.GrandTotal);
                    report.ChildsInfo.Add(liveTypeWinLoss);
                }

                report.GrandTotal.Margin = this.CalculateMargin(
                    report.GrandTotal.RealIncome,
                    report.GrandTotal.RealTurnover);
                report.GrandTotal = this.FormatWinLossInfo(report.GrandTotal);
            }

            return report;
        }

        public Dictionary<object, string> GetSportName(
            List<dynamic> sportIdList,
            ref Dictionary<object, string> currentStorage)
        {
            currentStorage = currentStorage ?? new Dictionary<object, string>();
            foreach (var sportIdInfo in sportIdList)
            {
                if (!currentStorage.ContainsKey(sportIdInfo))
                {
                    var name = string.Empty;
                    int id = Converter.ToInt(sportIdInfo);
                    switch (id)
                    {
                        case -1:
                            name = "Mixed Sports";
                            break;

                        case -2:
                            name = "Mixed Other Sports";
                            break;

                        default:
                            name = CacheRef.GetSportName(id);
                            break;
                    }

                    currentStorage.Add(sportIdInfo, name);
                }
            }

            return currentStorage;
        }
        
        private string ConvertCombinationType(string combination)
        {
            var name = string.Empty;

            if (!string.IsNullOrEmpty(combination) &&
                combination.Contains(DailyMixParlayConstant.DailyMixParlayReportCombinationPrefix))
            {
                var combinationSubString = combination.Substring(DailyMixParlayConstant.DailyMixParlayReportCombinationPrefix.Length);

                name = $"{combinationSubString}XCombinations{string.Empty}";

                if (string.Equals(
                    combination,
                    DailyMixParlayConstant.DailyMixParlayReportMaxCombination,
                    StringComparison.OrdinalIgnoreCase))
                {
                    name = $"{name}{DailyMixParlayConstant.MaxCombinationSuffix}";  // Exp: 2XCombinations & up
                }
            }
            else
            {
                name = DailyMixParlayConstant.Unknow;
            }

            return name;
        }

        private string ConvertLiveType(int liveType)
        {
            string name = string.Empty;
            switch (liveType)
            {
                case LiveTypeConstants.NonLive:
                    name = Labels.Non_Live;
                    break;

                case LiveTypeConstants.Live:
                    name = Labels.Live;
                    break;

                case LiveTypeConstants.MixLiveTypes:
                    name = Labels.Mix_Live_and_Non_Live;
                    break;
            }

            return name;
        }

        private string GetSportName(int sportType)
        {
            var sportTotalName = string.Empty;
            switch (sportType)
            {
                case SportContants.Soccer:
                    sportTotalName = Labels.Soccer + " " + Labels.Sport;
                    break;

                case SportContants.CombinationMixParlay:
                    sportTotalName = Labels.Multi + " " + Labels.Sport;
                    break;

                default:
                    sportTotalName = Labels.Others + " " + Labels.Sport;
                    break;
            }

            return sportTotalName;
        }
    }
}
