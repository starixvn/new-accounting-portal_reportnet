namespace HouseWinLoss.Soccer.GroupBetType;

using BaseService;

public class GroupBetTypeWinLossModel : BaseBetTypeWinLossModel
{
    public List<WinLossSummaryByBetTypeGroup> SiteWinLossSummaries { get; set; }
}