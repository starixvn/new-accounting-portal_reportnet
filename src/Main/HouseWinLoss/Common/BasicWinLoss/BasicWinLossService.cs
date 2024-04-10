using HouseWinLoss.Common.Criteria;
using HouseWinLoss.Common.Services;

namespace HouseWinLoss.Common.BasicWinLoss
{
    public interface BasicWinLossService
    {
        Task<BasicWinLossModel> Query(WinLossCriteria criteria);
    }

    public class BasicWinLossServiceImp : BasicWinLossService
    {
        private readonly RawWinLossService rawWinLossService;

        private readonly CalculationService calculationService;

        public BasicWinLossServiceImp(
            RawWinLossService rawWinLossService,
            CalculationService calculationService)
        {
            this.rawWinLossService = rawWinLossService;
            this.calculationService = calculationService;
        }

        public async Task<BasicWinLossModel> Query(WinLossCriteria criteria)
        {
            var rawWinLosses = await rawWinLossService.Query(criteria);

            var siteWinLossSummaries = calculationService.CalculateSiteWinLossSummary(rawWinLosses);
            var grandTotal = calculationService.CalculateSumWinLossSummary("Grand Total", rawWinLosses);
            var allRMData = calculationService.CalculateRM(rawWinLosses);

            return new BasicWinLossModel
            {
                SiteWinLossSummaries = siteWinLossSummaries.ToList(),
                RM = allRMData.ToList(),
                GrandTotal = grandTotal
            };
        }
    }
}