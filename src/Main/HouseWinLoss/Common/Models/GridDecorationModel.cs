namespace HouseWinLoss.Common.Models;

public class GridDecorationModel
{
    private const int DefaultMainColumnWidth = 180;

    public string Title { get; set; } = string.Empty;

    public string LiveTitle { get; set; } = string.Empty;

    public string NonLiveTitle { get; set; } = string.Empty;

    public string MainColumnText { get; set; } = string.Empty;

    public int MainColumnWidth { get; set; } = DefaultMainColumnWidth;

    public string SheetName { get; set; }
}