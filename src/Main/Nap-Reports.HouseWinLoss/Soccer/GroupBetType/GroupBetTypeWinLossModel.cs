using System.Collections.Generic;
using Nap_Reports.HouseWinLoss.Soccer.BaseService;

namespace Nap_Reports.HouseWinLoss.Soccer.GroupBetType
{
    public class GroupBetTypeWinLossModel : BaseBetTypeWinLossModel
    {
        public List<WinLossSummaryByBetTypeGroup> SiteWinLossSummaries { get; set; }
    }
}