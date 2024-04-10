using Fanex.Excel.Models;

namespace HouseWinLoss.DailyWinLoss2hSoccerMinor.MatchDetail
{
    public interface SoccerMinorMatchGridOptionColumnDecorator
    {
        GridOptions Build(GridOptions gridOptions, string sortColumnName, string sortDirection);
    }
}