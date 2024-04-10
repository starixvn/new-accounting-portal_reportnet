using HouseWinLoss.Soccer.BaseService;

namespace HouseWinLoss.Soccer.GroupBetType
{
    public class GroupBetTypeWinLossModel : BaseBetTypeWinLossModel
    {
        public List<WinLossSummaryByBetTypeGroup> SiteWinLossSummaries { get; set; }
    }
}