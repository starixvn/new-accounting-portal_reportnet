using Fanex.Excel.Enums;
using Fanex.Excel.Models;

namespace HouseWinLoss.Common.ColumnDecorators;

public class MatchGridOptionColumnDecoratorFull(GridOptionColumnRemoval columnRemoval)
    : GridOptionColumnDecoratorBase(columnRemoval), MatchGridOptionColumnDecorator
{
    public GridOptions Build(GridOptions gridOptions)
    {
        gridOptions.CustomizeHeader = true;
        gridOptions.Columns = BuildFullMatchColumns();
        gridOptions.GroupHeaderRows = BuildFullMatchGroupHeaderRows();

        return gridOptions;
    }

    private List<HeaderColumns> BuildFullMatchColumns()
    {
        var headerColumns = new List<HeaderColumns>
        {
            new() { HeaderText = "Code", Width = 130, Name = "MatchCode", CellAlignment = HAlignment.Center, Bold = true },
            new() { HeaderText = string.Empty, Width = 150, Name = "Home", CellAlignment = HAlignment.Right },
            new() { HeaderText = string.Empty, Width = 20, Name = "VS", CellAlignment = HAlignment.Center },
            new() { HeaderText = string.Empty, Width = 150, Name = "Away", CellAlignment = HAlignment.Left }
        };

        var baseHeaderColumns = CreateCoreHeaderColumns();
        headerColumns.AddRange(baseHeaderColumns);

        return headerColumns;
    }

    private List<GroupHeaderRows> BuildFullMatchGroupHeaderRows()
    {
        var columns = new List<HeaderColumns>
        {
            new() { HeaderText = "Code", Name = "MatchCode", Width = 130, MergeRowNumber = 2 },
            new() { HeaderText = "Event", Name = "Home", Width = 320, MergeColNumber = 3, MergeRowNumber = 2 },
            new() { HeaderText = "Member Win Loss", Name = "MemberWinLoss", Width = 100, MergeRowNumber = 2 },
            new() { HeaderText = "SMA Win Loss", Name = "SMAWinLoss",  Width = 100, MergeRowNumber = 2 },
            new() { HeaderText = "Extra PT", Name = "ExtraPT",  Width = 100, MergeRowNumber = 2 },
            new() { HeaderText = "Commission", Name = "Commission",  Width = 100, MergeRowNumber = 2 },
            new() { HeaderText = "House", Name = "RealIncome", Width = 100, MergeColNumber = 3 },
            new() { HeaderText = "House Turnover", Name = "VirtualTurnover",  Width = 100, MergeColNumber = 2 },
            new() { HeaderText = "Margin", Name = "VirtualMargin",  Width = 100, MergeColNumber = 2 }
        };

        var groupHeader = new GroupHeaderRows { Columns = columns };
        return [groupHeader];
    }
}