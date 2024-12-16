namespace TreeWinLoss.Model;

public class TreeWinLossModel
{
    public List<TreeWinLossItemModel> WinLosses { get; set; }

    public List<TreeWinLossItemModel> Total { get; set; }

    public bool IsShowNotAvailableTurnover { get; set; }
}