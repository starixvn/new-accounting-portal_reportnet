using Fanex.Excel.Models;

namespace HouseWinLoss.DailyWinLoss2hSoccerMinor.BetType
{
    public interface BettypeGridOptionColumnDecorator
    {
        GridOptions Build(GridOptions gridOptions, string mainColumnText, string sortColumnName, string sortDirection, int mainColumnWidth = 180);
    }
}
