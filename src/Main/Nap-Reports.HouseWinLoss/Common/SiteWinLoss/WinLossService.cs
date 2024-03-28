using System.Linq;
using Nap_Reports.HouseWinLoss.Common.Criteria;
using Nap_Reports.HouseWinLoss.Common.Services;

namespace Nap_Reports.HouseWinLoss.Common.SiteWinLoss
{
    public interface WinLossService
    {
        Task<SiteWinLossModel> Query(WinLossCriteria criteria);
    }

    public class WinLossServiceImp : WinLossService
    {
        private readonly RawWinLossService rawWinLossService;

        private readonly CalculationService calculationService;

        public WinLossServiceImp(
            RawWinLossService rawWinLossService,
            CalculationService calculationService)
        {
            this.rawWinLossService = rawWinLossService;
            this.calculationService = calculationService;
        }

        public async Task<SiteWinLossModel> Query(WinLossCriteria criteria)
        {
            var rawWinLosses = await rawWinLossService.Query(criteria);

            var siteWinLosses = rawWinLosses.Select(r => calculationService.FromRawWinLossByRowName(r));
            var grandTotal = calculationService.CalculateSumWinLoss("Grand Total", rawWinLosses);

            return new SiteWinLossModel
            {
                Sites = siteWinLosses.ToList(),
                Total = grandTotal
            };
        }
    }
}