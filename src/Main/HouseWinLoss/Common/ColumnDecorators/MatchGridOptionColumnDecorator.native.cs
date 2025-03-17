using Fanex.Excel.Models;

namespace HouseWinLoss.Common.ColumnDecorators;

public interface MatchGridOptionColumnDecorator
{
    GridOptions Build(GridOptions gridOptions, string sortColumnName, string sortDirection);
}