namespace HouseWinLoss.Soccer.BaseService;

using Common.Models;

public class WinLossSummaryByBetTypeGroup
{
    public List<SiteWinLossSummary> SiteWinLossSummaries { get; set; }

    public WinLoss Total { get; set; }
}