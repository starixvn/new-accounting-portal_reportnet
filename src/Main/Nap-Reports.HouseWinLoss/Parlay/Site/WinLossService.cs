using System.Collections.Generic;
using System.Linq;
using Nap_Reports.HouseWinLoss.Common.Criteria;
using Nap_Reports.HouseWinLoss.Common.Models;
using Nap_Reports.HouseWinLoss.Common.Services;

namespace Nap_Reports.HouseWinLoss.Parlay.Site
{
    public interface WinLossService
    {
        Task<SiteParlayModel> Query(WinLossCriteria criteria);
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

        public async Task<SiteParlayModel> Query(WinLossCriteria criteria)
        {
            var rawWinLosses = await rawWinLossService.Query(criteria);

            var groups = rawWinLosses.Select(r => new { Name = r.GroupName }).Distinct();
            var sites = from @group in groups
                        let rawGroupWinLosses = rawWinLosses.Where(r => r.GroupName == @group.Name).ToArray()
                        select CalculationSiteTotal(@group.Name, rawGroupWinLosses);

            var grandTotal = calculationService.CalculateSumWinLoss("Grand Total", rawWinLosses);

            return new SiteParlayModel
            {
                Sites = sites.ToList(),
                Total = grandTotal
            };
        }

        private SiteModel CalculationSiteTotal(string name, IEnumerable<WinLossRaw> rawWinLosses)
        {
            var siteWinLosses = rawWinLosses.Select(r => calculationService.FromRawWinLossByRowName(r)).ToList();
            var siteTotal = calculationService.CalculateSumWinLoss(name, rawWinLosses);
            return new SiteModel
            {
                Sites = siteWinLosses,
                Total = siteTotal
            };
        }
    }
}