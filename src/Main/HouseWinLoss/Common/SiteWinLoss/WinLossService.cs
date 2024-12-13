using HouseWinLoss.Common.Criteria;
using HouseWinLoss.Common.Services;

namespace HouseWinLoss.Common.SiteWinLoss
{
    public interface WinLossService
    {
        Task<SiteWinLossModel> Query(WinLossCriteria criteria);
    }

    public class WinLossServiceImp(
        RawWinLossService rawWinLossService,
        CalculationService calculationService)
        : WinLossService
    {
        public async Task<SiteWinLossModel> Query(WinLossCriteria criteria)
        {
            var rawWinLosses = await rawWinLossService.Query(criteria);

            var siteWinLosses = rawWinLosses.Select(calculationService.FromRawWinLossByRowName);
            var grandTotal = calculationService.CalculateSumWinLoss("Grand Total", rawWinLosses);

            return new SiteWinLossModel
            {
                Sites = siteWinLosses.ToList(),
                Total = grandTotal
            };
        }
    }
}