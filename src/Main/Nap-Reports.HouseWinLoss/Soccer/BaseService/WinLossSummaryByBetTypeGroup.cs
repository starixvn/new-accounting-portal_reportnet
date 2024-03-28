using System.Collections.Generic;
using Nap_Reports.HouseWinLoss.Common.Models;

namespace Nap_Reports.HouseWinLoss.Soccer.BaseService
{
    public class WinLossSummaryByBetTypeGroup
    {
        public List<SiteWinLossSummary> SiteWinLossSummaries { get; set; }

        public WinLoss Total { get; set; }
    }
}