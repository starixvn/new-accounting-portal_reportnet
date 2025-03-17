using Fanex.Excel.Enums;
using Fanex.Excel.Models;

namespace HouseWinLoss.Common.ColumnDecorators;

public class MatchGridOptionColumnDecoratorVirtualSport(GridOptionColumnRemoval columnRemoval)
    : GridOptionColumnDecoratorBase(columnRemoval), MatchGridOptionColumnDecorator
{
    public GridOptions Build(GridOptions gridOptions, string sortColumnName, string sortDirection)
    {
        gridOptions.BodyRows[0].Columns[1].MergeColNumber -= 2;
        gridOptions.CustomizeHeader = true;
        gridOptions.Columns = BuildBasicMatchColumns();
        gridOptions.GroupHeaderRows = BuildBasicMatchGroupHeaderRows();

        return gridOptions;
    }

    private static List<HeaderColumns> BuildBasicMatchColumns()
    {
        return
        [
            new HeaderColumns
            {
                HeaderText = "Code",
                Width = 120,
                Name = "MatchCode",
                CellAlignment = HAlignment.Center,
                Bold = true
            },
            new HeaderColumns { HeaderText = "Event", Width = 430, Name = "Home", CellAlignment = HAlignment.Right },
            new HeaderColumns
            {
                HeaderText = "Member Win Loss",
                Width = 100,
                Name = "MemberWinLoss",
                CellAlignment = HAlignment.Right,
                CustomCellType = ExcelCellType.DoubleRedPositive
            },
            new HeaderColumns
            {
                HeaderText = "SMA Win Loss",
                Width = 100,
                Name = "SMAWinLoss",
                CellAlignment = HAlignment.Right,
                CustomCellType = ExcelCellType.Double
            },
            new HeaderColumns
            {
                HeaderText = "Extra PT",
                Width = 100,
                Name = "ExtraPT",
                CellAlignment = HAlignment.Right,
                CustomCellType = ExcelCellType.Double
            },
            new HeaderColumns
            {
                HeaderText = "Commission",
                Width = 100,
                Name = "Commission",
                CellAlignment = HAlignment.Right,
                CustomCellType = ExcelCellType.Double
            },
            new HeaderColumns
            {
                HeaderText = "House Real Income",
                Width = 120,
                Name = "RealIncome",
                CellAlignment = HAlignment.Right,
                CustomCellType = ExcelCellType.DoubleRedPositive
            },
            new HeaderColumns
            {
                HeaderText = "House Real Turnover",
                Width = 120,
                Name = "RealTurnover",
                CellAlignment = HAlignment.Right,
                CustomCellType = ExcelCellType.Double
            },
            new HeaderColumns
            {
                HeaderText = "Margin",
                Width = 100,
                Name = "RealMargin",
                CellAlignment = HAlignment.Right,
                CustomCellType = ExcelCellType.PercentageRedPositive
            }
        ];
    }

    private static List<GroupHeaderRows> BuildBasicMatchGroupHeaderRows()
    {
        var columns = new List<HeaderColumns>
            {
                new() { HeaderText = "Code", Name = "MatchCode", Width = 120, MergeRowNumber = 2 },
                new() { HeaderText = "Event", Name = "Home", Width = 430, MergeRowNumber = 2 },
                new() { HeaderText = "Member Win Loss", Name = "MemberWinLoss", Width = 100, MergeRowNumber = 2 },
                new() { HeaderText = "SMA Win Loss", Name = "SMAWinLoss",  Width = 100, MergeRowNumber = 2 },
                new() { HeaderText = "Extra PT", Name = "ExtraPT",  Width = 120, MergeRowNumber = 2 },
                new() { HeaderText = "Commission", Name = "Commission",  Width = 100, MergeRowNumber = 2 },
                new() { HeaderText = "House Real Income", Name = "RealIncome", Width = 120, MergeRowNumber = 2 },
                new() { HeaderText = "House Real Turnover", Name = "RealTurnover",  Width = 120, MergeRowNumber = 2 },
                new() { HeaderText = "Margin", Name = "RealMargin",  Width = 100, MergeRowNumber = 2 }
            };

        var groupHeader = new GroupHeaderRows { Columns = columns };
        return [groupHeader];
    }
}