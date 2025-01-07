using HouseWinLoss.Common.Models;

namespace HouseWinLoss.Soccer.BaseService;

public class BaseBetTypeWinLossModel
{
    protected BaseBetTypeWinLossModel()
    {
    }

    public SiteWinLossSummary GrandTotal { get; set; }

    public WinLoss MainTotal { get; set; }

    public WinLoss OtherTotal { get; set; }

    public WinLoss HedgeTotal { get; set; }

    public List<WinLoss> RM { get; set; } = [];
}