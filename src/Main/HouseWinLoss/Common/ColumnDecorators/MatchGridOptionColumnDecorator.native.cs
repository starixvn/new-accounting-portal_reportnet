namespace HouseWinLoss.Common.ColumnDecorators;

using Fanex.Excel.Models;

public interface MatchGridOptionColumnDecorator
{
    GridOptions Build(GridOptions gridOptions, string sortColumnName, string sortDirection);
}