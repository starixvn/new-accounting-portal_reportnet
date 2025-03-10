namespace HouseWinLoss.Common.BasicWinLoss;

using Models;

public class BasicWinLossModel
{
    public List<SiteWinLossSummary> SiteWinLossSummaries { get; set; }

    public List<WinLoss> RM { get; set; } = [];

    public SiteWinLossSummary GrandTotal { get; set; }
}