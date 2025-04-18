namespace HouseWinLoss.Soccer.SumBetType;

using BaseService;
using Common.Models;

public class SumBetTypeWinLossModel : BaseBetTypeWinLossModel
{
    public List<SiteWinLossSummary> SiteWinLossSummaries { get; set; }
}