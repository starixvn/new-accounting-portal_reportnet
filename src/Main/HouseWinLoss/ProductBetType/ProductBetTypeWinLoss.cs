using HouseWinLoss.Common.Models;

namespace HouseWinLoss.ProductBetType
{
    public class ProductBetTypeWinLoss
    {
        public IEnumerable<WinLoss> BetTypes { get; set; }

        public WinLoss GrandTotal { get; set; }
    }
}
