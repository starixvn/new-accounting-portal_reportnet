using Fanex.Excel.Enums;
using Fanex.Excel.Models;
using HouseWinLoss.Common.BasicWinLoss;
using HouseWinLoss.Common.Models;

namespace HouseWinLoss.Soccer.BaseService
{
    public class BaseBetTypeGridOptionsBuilder : BaseGridOptionsBuilder
    {
        const string grandTotalClass = "grand-total";

        protected BaseBetTypeGridOptionsBuilder(BasicGridDataRowBuilder gridDataRowBuilder) : base(gridDataRowBuilder)
        {
        }

        protected List<GridDataRow> BuildFooter(BaseBetTypeWinLossModel winLoss)
        {
            var rows = new List<WinLoss>
            {
                winLoss.MainTotal,
                winLoss.OtherTotal,
                winLoss.HedgeTotal
            };

            var footer = BuildRowsFooter(rows);
            footer.Add(BuildGrandTotal(winLoss.GrandTotal));
            footer.AddRange(BuildRowsFooter(winLoss.RM));
            rows.AddRange(winLoss.RM);

            return footer;
        }

        protected List<GridDataRow> BuildRowsFooter(IEnumerable<WinLoss> rows)
        {
            return rows
                .Where(r => r.RealTurnover != 0 || r.VirtualTurnover != 0)
                .Select(r => GridDataRowBuilder.BuildDataRow(r, HAlignment.Right))
                .ToList();
        }

        protected GridDataRow BuildGrandTotal(SiteWinLossSummary item)
        {
            var row = GridDataRowBuilder.BuildDataRow(item.Total, HAlignment.Right);
            row.RowType = RowType.ParentLevelOne;
            row.CssClass = grandTotalClass;

            var creditRow = GridDataRowBuilder.BuildDataRow(item.Credit, HAlignment.Right);
            creditRow.CssClass = grandTotalClass;

            var depositRow = GridDataRowBuilder.BuildDataRow(item.Deposit, HAlignment.Right);
            depositRow.CssClass = grandTotalClass;

            var cn88Row = GridDataRowBuilder.BuildDataRow(item.CN88, HAlignment.Right);
            cn88Row.CssClass = grandTotalClass;

            row.SubRows.Add(creditRow);
            row.SubRows.Add(depositRow);
            row.SubRows.Add(cn88Row);

            return row;
        }
    }
}