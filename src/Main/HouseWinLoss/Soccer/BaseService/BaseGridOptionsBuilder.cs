namespace HouseWinLoss.Soccer.BaseService;

using Common.BasicWinLoss;
using Common.Models;
using Fanex.Excel.Enums;
using Fanex.Excel.Models;

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

        row.SubRows =
        [
            GridDataRowBuilder.BuildDataRow(item.Credit, HAlignment.Right),
            GridDataRowBuilder.BuildDataRow(item.Deposit, HAlignment.Right),
            GridDataRowBuilder.BuildDataRow(item.CN88, HAlignment.Right)
        ];

        return row;
    }
}