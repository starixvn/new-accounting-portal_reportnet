using HouseWinLoss.Common.Models;
using HouseWinLoss.Common.Services;
using Utilities.RemotingRef;

namespace HouseWinLoss.Soccer.BaseService
{
    using AutoMapper;

    public interface BaseCalculationService : CalculationService
    {
        List<WinLossSummaryByBetTypeGroup> CalculateSiteWinLossSummaryGroups(IEnumerable<WinLossRaw> rawWinLosses);

        WinLoss CalculateMainSumWinLossSummary(string sumTitle, IEnumerable<WinLossRaw> rawWinLosses);

        WinLoss CalculateOtherSumWinLossSummary(string sumTitle, IEnumerable<WinLossRaw> rawWinLosses);

        WinLoss CalculateHedgeSumWinLossSummary(string sumTitle, IEnumerable<WinLossRaw> rawWinLosses);
    }

    public class BaseCalculationServiceImp(RemotingRefService refService, IMapper mapper)
        : CalculationServiceImp(refService, mapper), BaseCalculationService
    {
        public List<WinLossSummaryByBetTypeGroup> CalculateSiteWinLossSummaryGroups(IEnumerable<WinLossRaw> rawWinLosses)
        {
            var groups = rawWinLosses.Select(r => new { Name = r.GroupName, Id = r.GroupId }).Distinct();
            var winLosses = from @group in groups
                            let rawGroupWinLosses = rawWinLosses.Where(r => r.GroupId == @group.Id && r.GroupName == @group.Name).ToArray()
                            select CalculateSiteWinLossSummary(@group.Id, @group.Name, rawGroupWinLosses);
            var winLossesByBetTypeGroup = CalculateSiteWinLossSummaryByBetTypeGroup(winLosses);

            return winLossesByBetTypeGroup;
        }

        public WinLoss CalculateMainSumWinLossSummary(string sumTitle, IEnumerable<WinLossRaw> rawWinLosses)
        {
            var mainRawWinLosses = rawWinLosses
                .Where(r => !r.GroupName.Contains("(hedge)"))
                .Where(r => Enum.IsDefined(typeof(GroupConstants.MainBetType), r.GroupId));
            return CalculateSumWinLoss(sumTitle, mainRawWinLosses);
        }

        public WinLoss CalculateOtherSumWinLossSummary(string sumTitle, IEnumerable<WinLossRaw> rawWinLosses)
        {
            var otherRawWinLosses = rawWinLosses
                .Where(r => !r.GroupName.Contains("(hedge)") && r.GroupName != "RM")
                .Where(r => !Enum.IsDefined(typeof(GroupConstants.MainBetType), r.GroupId));
            return CalculateSumWinLoss(sumTitle, otherRawWinLosses);
        }

        public WinLoss CalculateHedgeSumWinLossSummary(string sumTitle, IEnumerable<WinLossRaw> rawWinLosses)
        {
            var hedgeRawWinLosses = rawWinLosses
                .Where(r => r.GroupName.Contains("(hedge)"));
            return CalculateSumWinLoss(sumTitle, hedgeRawWinLosses);
        }

        private List<WinLossSummaryByBetTypeGroup> CalculateSiteWinLossSummaryByBetTypeGroup(IEnumerable<SiteWinLossSummary> winLosses)
        {
            var winLossesGroup1 = winLosses.Where(r => Enum.IsDefined(typeof(GroupConstants.BetTypeGroup1), r.Total.Id));
            var winLossesGroup2 = winLosses.Where(r => Enum.IsDefined(typeof(GroupConstants.BetTypeGroup2), r.Total.Id));
            var winLossesGroup3 = winLosses.Where(r => Enum.IsDefined(typeof(GroupConstants.BetTypeGroup3), r.Total.Id));
            var winLossesGroup4 = winLosses.Where(r => !GroupConstants.BetTypeGroup123().Contains(r.Total.Id));

            var winLossesByGroupList = new List<WinLossSummaryByBetTypeGroup>();
            winLossesByGroupList = CreateWinLossByGroupList(winLossesByGroupList, winLossesGroup1);
            winLossesByGroupList = CreateWinLossByGroupList(winLossesByGroupList, winLossesGroup2);
            winLossesByGroupList = CreateWinLossByGroupList(winLossesByGroupList, winLossesGroup3);
            winLossesByGroupList = CreateWinLossByGroupList(winLossesByGroupList, winLossesGroup4);

            return winLossesByGroupList;
        }

        private List<WinLossSummaryByBetTypeGroup> CreateWinLossByGroupList(
            List<WinLossSummaryByBetTypeGroup> winLossesByGroupList,
            IEnumerable<SiteWinLossSummary> winLossesGroup)
        {
            if (winLossesGroup.Any())
            {
                winLossesByGroupList.Add(CreateWinLossSummaryByBetTypeGroup(winLossesGroup));
            }

            return winLossesByGroupList;
        }

        private WinLossSummaryByBetTypeGroup CreateWinLossSummaryByBetTypeGroup(IEnumerable<SiteWinLossSummary> winLossesGroup)
        {
            return new WinLossSummaryByBetTypeGroup
            {
                SiteWinLossSummaries = winLossesGroup.ToList(),
                Total = CalculateSumWinLoss("TOTAL", winLossesGroup.Select(r => r.Total))
            };
        }

        private WinLoss CalculateSumWinLoss(string sumTitle, IEnumerable<WinLoss> winLosses)
        {
            var totalWinLoss = CalculateSum(winLosses);
            totalWinLoss.Name = sumTitle;

            return totalWinLoss;
        }

        private WinLoss CalculateSum(IEnumerable<WinLoss> winLosses)
        {
            return new WinLoss
            {
                Commission = winLosses.Sum(r => r.Commission),
                MemberWinLoss = winLosses.Sum(r => r.MemberWinLoss),
                RealIncome = winLosses.Sum(r => r.RealIncome),
                RealTurnover = winLosses.Sum(r => r.RealTurnover),
                SMAWinLoss = winLosses.Sum(r => r.SMAWinLoss),
                VirtualIncome = winLosses.Sum(r => r.VirtualIncome),
                VirtualTurnover = winLosses.Sum(r => r.VirtualTurnover),
                Variance = winLosses.Sum(r => r.Variance),
                ExtraPT = winLosses.Sum(r => r.ExtraPT)
            };
        }
    }
}