using HouseWinLoss.Common.Criteria;
using HouseWinLoss.Common.Services;

namespace HouseWinLoss.Soccer.SumBetType
{
    public interface SumBetTypeWinLossService
    {
        Task<SumBetTypeWinLossModel> Query(WinLossCriteria criteria);
    }

    public class SumBetTypeWinLossServiceImp(
        RawWinLossService rawWinLossService,
        BaseService.BaseCalculationService calculationService)
        : SumBetTypeWinLossService
    {
        public async Task<SumBetTypeWinLossModel> Query(WinLossCriteria criteria)
        {
            var rawWinLosses = await rawWinLossService.Query(criteria);

            var betTypeWinLosses = calculationService.CalculateSiteWinLossSummary(rawWinLosses);
            var mainTotal = calculationService.CalculateMainSumWinLossSummary("Main Bet Types Total", rawWinLosses);
            var otherTotal = calculationService.CalculateOtherSumWinLossSummary("Other Bet Types Total", rawWinLosses);
            var hedgeTotal = calculationService.CalculateHedgeSumWinLossSummary("Hedge Total", rawWinLosses);
            var grandTotal = calculationService.CalculateSumWinLossSummary("Grand Total", rawWinLosses);
            var allRMData = calculationService.CalculateRM(rawWinLosses);

            return new SumBetTypeWinLossModel
            {
                SiteWinLossSummaries = betTypeWinLosses.ToList(),
                MainTotal = mainTotal,
                OtherTotal = otherTotal,
                HedgeTotal = hedgeTotal,
                GrandTotal = grandTotal,
                RM = allRMData.ToList()
            };
        }
    }
}