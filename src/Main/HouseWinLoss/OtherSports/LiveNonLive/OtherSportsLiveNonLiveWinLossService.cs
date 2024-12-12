using HouseWinLoss.Common.BasicWinLoss;
using HouseWinLoss.Common.Criteria;
using HouseWinLoss.Common.Services;

namespace HouseWinLoss.OtherSports.LiveNonLive
{
    public interface OtherSportsLiveNonLiveWinLossService
    {
        Task<IEnumerable<BasicWinLossModel>> Query(WinLossCriteria criteria);
    }

    public class OtherSportsLiveNonLiveWinLossServiceImp(
        RawWinLossService rawWinLossService,
        CalculationService calculationService)
        : OtherSportsLiveNonLiveWinLossService
    {
        public async Task<IEnumerable<BasicWinLossModel>> Query(WinLossCriteria criteria)
        {
            var rawWinLosses = await rawWinLossService.Query(criteria);

            var sportTypeRawWinLosses = rawWinLosses.Where(r => !r.GroupName.Equals("Other Sports Live", StringComparison.CurrentCultureIgnoreCase)
                                                                && !r.GroupName.Equals("Other Sports Non-Live", StringComparison.CurrentCultureIgnoreCase));
            var sportTypeSiteWinLossSummaries = calculationService.CalculateSiteWinLossSummary(sportTypeRawWinLosses);
            var sportTypeGrandTotal = calculationService.CalculateSumWinLossSummary("Grand Total", sportTypeRawWinLosses);
            var sportTypeAllRMData = calculationService.CalculateRM(rawWinLosses);

            var sportTypeSummaries = new BasicWinLossModel
            {
                SiteWinLossSummaries = sportTypeSiteWinLossSummaries.ToList(),
                RM = sportTypeAllRMData.ToList(),
                GrandTotal = sportTypeGrandTotal
            };

            var liveNonLiveRawWinLosses = rawWinLosses.Where(r => r.GroupName.Equals("Other Sports Live", StringComparison.CurrentCultureIgnoreCase)
                                                                || r.GroupName.Equals("Other Sports Non-Live", StringComparison.CurrentCultureIgnoreCase));
            var liveNonLiveSiteWinLossSummaries = calculationService.CalculateSiteWinLossSummary(liveNonLiveRawWinLosses);
            var liveNonLiveGrandTotal = calculationService.CalculateSumWinLossSummary("Grand Total", liveNonLiveRawWinLosses);
            var liveNonLiveAllRMData = calculationService.CalculateRM(rawWinLosses);

            var liveNonLiveSummaries = new BasicWinLossModel
            {
                SiteWinLossSummaries = liveNonLiveSiteWinLossSummaries.ToList(),
                RM = liveNonLiveAllRMData.ToList(),
                GrandTotal = liveNonLiveGrandTotal
            };

            return [sportTypeSummaries, liveNonLiveSummaries];
        }
    }
}
