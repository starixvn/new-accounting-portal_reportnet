using Fanex.Excel.Models;

namespace HouseWinLoss.Soccer.BetTypeFull
{
    public interface GridOptionFactory
    {
        Task<GridOptions> CreateGridOption(ExcelQueryModel queryModel, string title, int isLive);
    }
}
