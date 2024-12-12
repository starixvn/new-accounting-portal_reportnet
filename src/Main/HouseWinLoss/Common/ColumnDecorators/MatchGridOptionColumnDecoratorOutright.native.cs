using Fanex.Excel.Enums;
using Fanex.Excel.Models;

namespace HouseWinLoss.Common.ColumnDecorators
{
    public class MatchGridOptionColumnDecoratorOutright(GridOptionColumnRemoval columnRemoval)
        : GridOptionColumnDecoratorBase(columnRemoval), MatchGridOptionColumnDecorator
    {
        public GridOptions Build(GridOptions gridOptions)
        {
            DecorateMatchRow(gridOptions);
            RemoveBodyAndFooterColumn(
                gridOptions,
                MatchDetailAwayIndex,
                MatchDetailVsIndex);
            gridOptions.CustomizeHeader = true;
            gridOptions.Columns = BuildOutrightColumns();
            gridOptions.GroupHeaderRows = BuildOutrightGroupHeaderRows();

            return gridOptions;
        }

        private List<HeaderColumns> BuildOutrightColumns()
        {
            var headerColumns = new List<HeaderColumns>
            {
                new() { HeaderText = "Code", Width = 130, Name = "MatchCode", CellAlignment = HAlignment.Center, Bold = true },
                new() { HeaderText = "Home", Width = 150, Name = "Home", CellAlignment = HAlignment.Right }
            };
            var baseHeaderColumns = CreateCoreHeaderColumns();
            headerColumns.AddRange(baseHeaderColumns);

            return headerColumns;
        }

        private List<GroupHeaderRows> BuildOutrightGroupHeaderRows()
        {
            var columns = new List<HeaderColumns>
            {
                new() { HeaderText = "Code", Name = "MatchCode", Width = 130, MergeRowNumber = 2 },
                new() { HeaderText = "Event", Name = "Home", Width = 150, MergeRowNumber = 2 },
                new() { HeaderText = "Member Win Loss", Name = "MemberWinLoss", Width = 100, MergeRowNumber = 2 },
                new() { HeaderText = "SMA Win Loss", Name = "SMAWinLoss",  Width = 100, MergeRowNumber = 2 },
                new() { HeaderText = "Extra PT", Name = "ExtraPT",  Width = 100, MergeRowNumber = 2 },
                new() { HeaderText = "Commission", Name = "Commission",  Width = 100, MergeRowNumber = 2 },
                new() { HeaderText = "House Real Income", Name = "RealIncome", Width = 300, MergeColNumber = 3 },
                new() { HeaderText = "House Real Turnover", Name = "RealTurnover",  Width = 200, MergeColNumber = 2 },
                new() { HeaderText = "Margin", Name = "VirtualMargin",  Width = 100, MergeColNumber = 2 }
            };

            var groupHeader = new GroupHeaderRows { Columns = columns };
            return [groupHeader];
        }

        private static void DecorateMatchRow(GridOptions gridOptions)
        {
            var matchRows = gridOptions
                .BodyRows
                .Where(r => r.Columns.Count > 1 && r.Columns[1].Name == "Home")
                .ToList();
            matchRows.ForEach(r =>
            {
                r.Columns[1].CellAlignment = HAlignment.Center;
                r.Columns[0].CustomAttributes = null;
            });
        }
    }
}