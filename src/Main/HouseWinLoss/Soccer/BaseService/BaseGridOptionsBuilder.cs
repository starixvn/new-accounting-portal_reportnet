using Fanex.Excel.Enums;
using Fanex.Excel.Models;
using HouseWinLoss.Common.BasicWinLoss;
using HouseWinLoss.Common.Models;

namespace HouseWinLoss.Soccer.BaseService
{
    public class BaseGridOptionsBuilder
    {
        protected BaseGridOptionsBuilder(BasicGridDataRowBuilder gridDataRowBuilder)
        {
            GridDataRowBuilder = gridDataRowBuilder;
        }

        protected BasicGridDataRowBuilder GridDataRowBuilder { get; }

        protected GridDataRow BuildBetTypeDataRow(SiteWinLossSummary item, HAlignment alignment)
        {
            var row = GridDataRowBuilder.BuildDataRow(item.Total, alignment);
            row.RowType = RowType.ParentLevelOne;

            row.SubRows = new List<GridDataRow>
            {
                GridDataRowBuilder.BuildDataRow(item.Credit, HAlignment.Right),
                GridDataRowBuilder.BuildDataRow(item.Deposit, HAlignment.Right),
                GridDataRowBuilder.BuildDataRow(item.CN88, HAlignment.Right)
            };

            return row;
        }
    }
}