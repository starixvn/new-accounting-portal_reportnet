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

        Task<WinLoss> GetAdvanceMixParlayReport(GetAdvanceMixParlayCriteria criteria);
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
                                           let combinationGroupRawData = rawData.Where(x => x.Combinations == combinationGroup.Key)
                                           select CalculateCombinationGroup(combinationGroup.Key, combinationGroupRawData);

                var total = rawData.DynamicSum();
                total.Name = "GrandTotal";

                report.WinLosses = combinationWinLosses.ToList();
                report.Total = total;

            }

            return report;
        }

        private ParentGroupItem CalculateCombinationGroup(string combination, IEnumerable<WinLossItem> rawData)
        {
            var combinationName = this.ConvertCombinationType(combination);

            var result = CalculateCombinationByLiveType(rawData);
            result.Name = combinationName;

            return result;
        }

        private ParentGroupItem CalculateCombinationByLiveType(IEnumerable<WinLossItem> rawData)
        {
            var liveTypeGroups = rawData.GroupBy(x => x.LiveType);

            var numberLiveTypeGroup = liveTypeGroups.Count();
            var numberRawDataItems = rawData.Count();

            var liveTypeWinLoss = from liveTypeGroup in liveTypeGroups
                                  let liveTypeRawData = rawData.Where(x => x.LiveType == liveTypeGroup.Key)
                                  select CalculateCombinationByLiveTypeGroup(liveTypeGroup.Key, liveTypeRawData);

            var total = CalculateSum(rawData, "Total");

            var mergeRowNumbers = numberLiveTypeGroup + numberRawDataItems + 1;

            return new ParentGroupItem
            {
                Name = string.Empty,
                WinLosses = liveTypeWinLoss.ToList(),
                Total = total,
                MergeRowNumber = mergeRowNumbers
            };
        }

        private WinLossItem CalculateSum(IEnumerable<WinLossItem> rawData, string totalType)
        {
            var total = rawData.DynamicSum();
            total.Name = totalType;

            return total;
        }
        private ChildGroupItem CalculateCombinationByLiveTypeGroup(bool liveType, IEnumerable<WinLossItem> rawData)
        {
            var winlosses = from item in rawData
                            select CalculateWinLossItem(item);

            var numberRawDataItems = rawData.Count();
            var mergeRowNumber = numberRawDataItems + 1;

            var total = CalculateSum(rawData, "Sub Total");

            return new ChildGroupItem
            {
                Name = liveType ? "Live" : "Non-Live",
                WinLosses = winlosses.ToList(),
                Total = total,
                MergeRowNumber = mergeRowNumber
            };
        }

        private WinLossItem CalculateWinLossItem(WinLossItem item)
        {
            var result = new WinLossItem
            {
                Name = $"Mix Parlay ({item.Status})",
                Id = item.Id,
                BetCount = item.BetCount,
                MemberTurnover = item.MemberTurnover,
                MemberWinLoss = item.MemberWinLoss,
                SMAWinLoss = item.SMAWinLoss,
                Commission = item.Commission,
                RealIncome = item.RealIncome,
                RealTurnover = item.RealTurnover,
                MemberBonus = item.MemberBonus,
                HouseBonus = item.HouseBonus,
                Status = item.Status,
                Combinations = item.Combinations,
                LiveType = item.LiveType,
                BetType = item.BetType,
                SportType = item.SportType,
                ExtraPT = item.ExtraPT
            };

            return result;
        }

        public async Task<WinLoss> GetAdvanceMixParlayReport(GetAdvanceMixParlayCriteria criteria)
        {
            var report = new WinLoss();

            var rawData = await repository.FetchAsync<WinLossItem>(criteria);

            if (rawData != null)
            {

                var liveTypeGroups = rawData.GroupBy(data => data.LiveType);

                var liveTypeWinLosses = from liveTypeGroup in liveTypeGroups
                                        let liveTypeGroupRawData = rawData.Where(x => x.LiveType == liveTypeGroup.Key)
                                        select CalculateLiveTypeGroup(liveTypeGroup.Key, liveTypeGroupRawData);

                var total = rawData.DynamicSum();
                total.Name = "GrandTotal";

                report.WinLosses = liveTypeWinLosses.ToList();
                report.Total = total;
            }

            return report;
        }

        private ParentGroupItem CalculateLiveTypeGroup(bool liveType, IEnumerable<WinLossItem> rawData)
        {
            var liveTypeName = liveType ? "Live" : "Non-Live";

            var result = CalculateParlayBySportType(rawData);
            result.Name = liveTypeName;

            return result;
        }

        private ParentGroupItem CalculateParlayBySportType(IEnumerable<WinLossItem> rawData)
        {
            var sportTypeGroups = rawData.GroupBy(x => x.SportType);

            var sportTypeWinLoss = from sportTypeGroup in sportTypeGroups
                                   let sportTypeRawData = rawData.Where(x => x.SportType == sportTypeGroup.Key)
                                   select CalculateParlayBySportTypeGroup(sportTypeGroup.Key, sportTypeRawData);

            var total = rawData.DynamicSum();
            total.Name = "Total";

            var numberGroup = sportTypeGroups.Count();
            var numberRawDataItems = rawData.Count();
            var mergeRowNumber = numberGroup + numberRawDataItems + 1;

            return new ParentGroupItem
            {
                Name = string.Empty,
                WinLosses = sportTypeWinLoss.ToList(),
                Total = total,
                MergeRowNumber = mergeRowNumber
            };
        }

        private ChildGroupItem CalculateParlayBySportTypeGroup(int sportType, IEnumerable<WinLossItem> rawData)
        {
            var total = rawData.DynamicSum();
            total.Name = "Sub Total";

            var winlosses = from item in rawData
                            select CalculateWinLossItem(item);

            var sportName = remotingRefService.GetSportName(sportType);
            
            var numberRawDataItems = rawData.Count();
            var mergeRowNumber = numberRawDataItems + 1;

            return new ChildGroupItem
            {
                Name = sportName,
                WinLosses = winlosses.ToList(),
                Total = total,
                MergeRowNumber = mergeRowNumber
            };
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
    }
}
