using Fanex.Excel.Enums;
using Fanex.Excel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nap_Reports.HouseWinLoss.Common.ColumnDecorators;

namespace Nap_Reports.HouseWinLoss.DailyWinLoss2hSoccerMinor.BetType
{
    public class BettypeGridOptionColumnDecoratorFull : GridOptionColumnDecoratorBase, BettypeGridOptionColumnDecorator
    {
        public BettypeGridOptionColumnDecoratorFull(GridOptionColumnRemoval columnRemoval) : base(columnRemoval)
        {
        }

        public GridOptions Build(GridOptions gridOptions, string mainColumnText, string sortColumnName, string sortDirection, int mainColumnWidth = 180)
        {
            gridOptions.CustomizeHeader = true;
            gridOptions.Columns = BuildFullColumns(mainColumnText, sortColumnName, sortDirection, mainColumnWidth);
            gridOptions.GroupHeaderRows = BuildFullGroupHeaderRows(mainColumnText, mainColumnWidth);

            return gridOptions;
        }

        private List<HeaderColumns> BuildFullColumns(string mainColumnText, string sortColumnName, string sortDirection, int mainColumnWidth = 180)
        {
            return new List<HeaderColumns>
            {
                new HeaderColumns { HeaderText = mainColumnText, Width = mainColumnWidth, Name = "Main", CellAlignment = HAlignment.Center, ShowCollapseExpand = true, IsBold = true},
                new HeaderColumns { HeaderText = "Member Win Loss", Width = 100, Name = "MemberWinLoss", CellAlignment = HAlignment.Right, CustomCellType = ExcelCellType.DoubleRedPositive },
                new HeaderColumns { HeaderText = "SMA Win Loss", Width = 100, Name = "SMAWinLoss", CellAlignment = HAlignment.Right, CustomCellType = ExcelCellType.Double },
                new HeaderColumns { HeaderText = "Extra PT", Width = 100, Name = "ExtraPT", CellAlignment = HAlignment.Right, CustomCellType = ExcelCellType.Double },
                new HeaderColumns { HeaderText = "Commission", Width = 100, Name = "Commission", CellAlignment = HAlignment.Right, CustomCellType = ExcelCellType.Double },
                new HeaderColumns {
                    HeaderText = "Variance",
                    Width = 100,
                    Name = "VarianceIncome",
                    CellAlignment = HAlignment.Right,
                    CustomCellType = ExcelCellType.DoubleRedPositive ,
                    Sort = true,
                    SortDirection = sortColumnName == "VarianceIncome" ? sortDirection : string.Empty
                },
                new HeaderColumns {
                    HeaderText = "Virtual Winloss",
                    Width = 120,
                    Name = "VirtualIncome",
                    CellAlignment = HAlignment.Right,
                    CustomCellType = ExcelCellType.DoubleRedPositive,
                    Sort = true,
                    SortDirection = sortColumnName == "VirtualIncome" ? sortDirection : string.Empty
                },
                new HeaderColumns {
                    HeaderText = "Real Income",
                    Width = 100,
                    Name = "RealIncome",
                    CellAlignment = HAlignment.Right,
                    CustomCellType = ExcelCellType.DoubleRedPositive,
                    Sort = true,
                    SortDirection = sortColumnName == "RealIncome" ? sortDirection : string.Empty
                },
                new HeaderColumns {
                    HeaderText = "Virtual",
                    Width = 100,
                    Name = "VirtualTurnover",
                    CellAlignment = HAlignment.Right,
                    CustomCellType = ExcelCellType.Double,
                    Sort = true,
                    SortDirection = sortColumnName == "VirtualTurnover" ? sortDirection : string.Empty
                },
                new HeaderColumns {
                    HeaderText = "Real",
                    Width = 100,
                    Name = "RealTurnover",
                    CellAlignment = HAlignment.Right,
                    CustomCellType = ExcelCellType.Double,
                    Sort = true,
                    SortDirection = sortColumnName == "RealTurnover" ? sortDirection : string.Empty
                },
                new HeaderColumns {
                    HeaderText = "Virtual",
                    Width = 100,
                    Name = "VirtualMargin",
                    CellAlignment = HAlignment.Right,
                    CustomCellType = ExcelCellType.PercentageRedPositive,
                    Sort = true,
                    SortDirection = sortColumnName == "VirtualMargin" ? sortDirection : string.Empty
                },
                new HeaderColumns {
                    HeaderText = "Real",
                    Width = 100,
                    Name = "RealMargin",
                    CellAlignment = HAlignment.Right,
                    CustomCellType = ExcelCellType.PercentageRedPositive,
                    Sort = true,
                    SortDirection = sortColumnName == "RealMargin" ? sortDirection : string.Empty}
            };
        }

        private List<GroupHeaderRows> BuildFullGroupHeaderRows(string mainColumnText, int mainColumnWidth = 180)
        {
            var columns = new List<HeaderColumns>
            {
                new HeaderColumns { HeaderText = mainColumnText, Name = "Main", MergeRowNumber = 2, Width = mainColumnWidth },
                new HeaderColumns { HeaderText = "Member Win Loss", Name = "MemberWinLoss", MergeRowNumber = 2, Width = 100 },
                new HeaderColumns { HeaderText = "SMA Win Loss", Name = "SMAWinLoss", MergeRowNumber = 2, Width = 100 },
                 new HeaderColumns { HeaderText = "Extra PT", Name = "ExtraPT", MergeRowNumber = 2, Width = 100 },
                new HeaderColumns { HeaderText = "Commission", Name = "Commission", MergeRowNumber = 2, Width = 100 },
                new HeaderColumns { HeaderText = "House", Name = "VarianceIncome", MergeColNumber = 3, Width = 100 },
                new HeaderColumns { HeaderText = "House Turnover", Name = "VirtualTurnover", MergeColNumber = 2, Width = 100 },
                new HeaderColumns { HeaderText = "Margin", Name = "VirtualMargin", MergeColNumber = 2, Width = 100 }
            };

            var groupHeader = new GroupHeaderRows { Columns = columns };
            return new List<GroupHeaderRows> { groupHeader };
        }
    }
}
