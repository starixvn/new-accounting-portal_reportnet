using HouseWinLoss.Common.Models;

namespace HouseWinLoss.Products;

public class ProductGroupWinLoss
{
    public List<SiteWinLossSummary> Products { get; set; }

    public SiteWinLossSummary Total { get; set; }

    public bool IsEmpty => Total.IsEmpty;
}