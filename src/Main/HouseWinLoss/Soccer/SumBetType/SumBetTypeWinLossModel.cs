using HouseWinLoss.Common.Models;
using HouseWinLoss.Soccer.BaseService;

namespace HouseWinLoss.Soccer.SumBetType
{
    public class SumBetTypeWinLossModel : BaseBetTypeWinLossModel
    {
        public List<SiteWinLossSummary> SiteWinLossSummaries { get; set; }
    }
}