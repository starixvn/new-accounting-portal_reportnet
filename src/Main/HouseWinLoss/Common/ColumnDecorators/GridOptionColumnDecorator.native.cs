namespace HouseWinLoss.Common.ColumnDecorators;

using Fanex.Excel.Models;

public interface GridOptionColumnDecorator
{
    GridOptions Build(GridOptions gridOptions, string mainColumnText, int mainColumnWidth = 180);
}