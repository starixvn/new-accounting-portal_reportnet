using Fanex.Excel.Models;

namespace HouseWinLoss.Common.ColumnDecorators
{
    public interface GridOptionColumnDecorator
    {
        GridOptions Build(GridOptions gridOptions, string mainColumnText, int mainColumnWidth = 180);
    }
}