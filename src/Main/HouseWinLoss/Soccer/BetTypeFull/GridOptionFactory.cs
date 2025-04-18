namespace HouseWinLoss.Soccer.BetTypeFull;

using Fanex.Excel.Models;

public interface GridOptionFactory
{
    Task<GridOptions> CreateGridOption(ExcelQueryModel queryModel, string title, int isLive);
}