using Fanex.Excel.Models;

namespace Nap_Reports.HouseWinLoss.Soccer.BetTypeFull
{
    public interface GridOptionFactory
    {
        Task<GridOptions> CreateGridOption(ExcelQueryModel queryModel, string title, int isLive);
    }
}
