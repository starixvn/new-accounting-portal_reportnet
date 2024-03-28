using Nap_Reports.HouseWinLoss.Common.BasicWinLoss;
using Nap_Reports.HouseWinLoss.Common.Criteria;
using Nap_Reports.HouseWinLoss.Common.Services;

namespace Nap_Reports.HouseWinLoss.MetricGaming.LiveNonLive
{
    public interface MetricGamingLiveNonLiveWinLossService
    {
        Task<IEnumerable<BasicWinLossModel>> Query(WinLossCriteria criteria);
    }

    public class MetricGamingLiveNonLiveWinLossServiceImp : MetricGamingLiveNonLiveWinLossService
    {
        private readonly RawWinLossService rawWinLossService;

        private readonly CalculationService calculationService;

        public MetricGamingLiveNonLiveWinLossServiceImp(
            RawWinLossService rawWinLossService,
            CalculationService calculationService)
        {
            this.rawWinLossService = rawWinLossService;
            this.calculationService = calculationService;
        }

        public async Task<IEnumerable<BasicWinLossModel>> Query(WinLossCriteria criteria)
        {
            var rawWinLosses = await rawWinLossService.Query(criteria);

            var betTypeRawWinLosses = rawWinLosses.Where(r => !r.GroupName.Equals("Metric Gaming - Superlive Live", StringComparison.CurrentCultureIgnoreCase)
                                                                && !r.GroupName.Equals("Metric Gaming - Superlive Non-Live", StringComparison.CurrentCultureIgnoreCase));
            var betTypeSiteWinLossSummaries = calculationService.CalculateSiteWinLossSummary(betTypeRawWinLosses);
            var betTypeGrandTotal = calculationService.CalculateSumWinLossSummary("Grand Total", betTypeRawWinLosses);

            var betTypeSummaries = new BasicWinLossModel
            {
                SiteWinLossSummaries = betTypeSiteWinLossSummaries.ToList(),
                GrandTotal = betTypeGrandTotal
            };

            var liveNonLiveRawWinLosses = rawWinLosses.Where(r => r.GroupName.Equals("Metric Gaming - Superlive Live", StringComparison.CurrentCultureIgnoreCase)
                                                                || r.GroupName.Equals("Metric Gaming - Superlive Non-Live", StringComparison.CurrentCultureIgnoreCase));
            var liveNonLiveSiteWinLossSummaries = calculationService.CalculateSiteWinLossSummary(liveNonLiveRawWinLosses);
            var liveNonLiveGrandTotal = calculationService.CalculateSumWinLossSummary("Grand Total", liveNonLiveRawWinLosses);

            var liveNonLiveSummaries = new BasicWinLossModel
            {
                SiteWinLossSummaries = liveNonLiveSiteWinLossSummaries.ToList(),
                GrandTotal = liveNonLiveGrandTotal
            };

            return new BasicWinLossModel[] { betTypeSummaries, liveNonLiveSummaries };
        }
    }
}