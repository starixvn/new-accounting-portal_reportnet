namespace HouseWinLoss.DailyWinLoss2hSoccerMinor.MatchDetail;

using Fanex.Excel.Models;

public interface SoccerMinorMatchGridOptionColumnDecorator
{
    GridOptions Build(GridOptions gridOptions, string sortColumnName, string sortDirection);
}