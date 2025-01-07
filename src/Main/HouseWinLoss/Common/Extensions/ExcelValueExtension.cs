namespace HouseWinLoss.Common.Extensions;

public static class ExcelValueExtension
{
    public static object ExcelRealMargin(this Models.WinLoss item)
    {
        return item.RealTurnover == 0 ? "N/A" : item.RealMargin;
    }

    public static object ExcelRealMargin(this MatchDetail.WinLoss item)
    {
        return item.RealTurnover == 0 ? "N/A" : item.RealMargin;
    }

    public static object ExcelVirtualMargin(this Models.WinLoss item)
    {
        return item.VirtualTurnover == 0 ? "N/A" : item.VirtualMargin;
    }

    public static object ExcelVirtualMargin(this MatchDetail.WinLoss item)
    {
        return item.VirtualTurnover == 0 ? "N/A" : item.VirtualMargin;
    }
}