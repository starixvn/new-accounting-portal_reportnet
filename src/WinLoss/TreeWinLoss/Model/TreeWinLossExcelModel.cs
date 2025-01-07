namespace TreeWinLoss.Model;

public class TreeWinLossByProductExcelModel
{
    public List<TreeWinLossItemByProductExcelModel> WinLosses { get; set; }

    public List<TreeWinLossItemByProductExcelModel> Total { get; set; }

    public bool IsShowNotAvailableTurnover { get; set; }
}