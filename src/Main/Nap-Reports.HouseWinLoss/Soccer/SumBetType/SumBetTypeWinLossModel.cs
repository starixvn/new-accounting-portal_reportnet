using System.Collections.Generic;
using Nap_Reports.HouseWinLoss.Common.Models;
using Nap_Reports.HouseWinLoss.Soccer.BaseService;

namespace Nap_Reports.HouseWinLoss.Soccer.SumBetType
{
    public class SumBetTypeWinLossModel : BaseBetTypeWinLossModel
    {
        public List<SiteWinLossSummary> SiteWinLossSummaries { get; set; }
    }
}