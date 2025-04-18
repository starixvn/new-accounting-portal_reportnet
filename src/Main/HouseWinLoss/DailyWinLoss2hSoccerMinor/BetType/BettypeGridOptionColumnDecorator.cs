namespace HouseWinLoss.DailyWinLoss2hSoccerMinor.BetType;

using Fanex.Excel.Models;

public interface BettypeGridOptionColumnDecorator
{
    GridOptions Build(GridOptions gridOptions, string mainColumnText, string sortColumnName, string sortDirection, int mainColumnWidth = 180);
}