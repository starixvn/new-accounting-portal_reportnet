namespace HouseWinLoss.ProductBetType;

using Common.Models;

public class ProductBetTypeWinLoss
{
    public IEnumerable<WinLoss> BetTypes { get; set; }

    public WinLoss GrandTotal { get; set; }
}