using Fanex.Excel.Models;

namespace Nap_Reports.HouseWinLoss.DailyWinLoss2hSoccerMinor.MatchDetail
{
    public interface SoccerMinorMatchGridOptionColumnDecorator
    {
        GridOptions Build(GridOptions gridOptions, string sortColumnName, string sortDirection);
    }
}