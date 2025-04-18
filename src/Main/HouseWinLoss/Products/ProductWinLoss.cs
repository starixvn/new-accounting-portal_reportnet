namespace HouseWinLoss.Products;

using Common.Models;

public class ProductWinLoss
{
    public ProductGroupWinLoss Sportsbook { get; set; }

    public List<WinLoss> RM { get; set; } = [];

    public ProductGroupWinLoss NonSportsbook { get; set; }

    public SiteWinLossSummary GrandTotal { get; set; }

    public WinLoss GrandTotalWithoutCN88 { get; set; }

    public bool IsEmpty => GrandTotal.Credit.IsEmpty
                           && GrandTotal.Deposit.IsEmpty
                           && GrandTotal.CN88.IsEmpty;
}