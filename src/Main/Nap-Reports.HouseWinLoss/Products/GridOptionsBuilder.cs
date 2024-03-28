using System.Collections.Generic;
using System.Linq;
using Fanex.Excel.Enums;
using Fanex.Excel.Models;
using Nap_Reports.HouseWinLoss.Common.BasicWinLoss;
using Nap_Reports.HouseWinLoss.Common.Models;

namespace Nap_Reports.HouseWinLoss.Products
{
    public interface GridOptionsBuilder
    {
        GridOptions Build(ProductWinLoss winLoss, string title);
    }

    public class GridOptionsBuilderImp : GridOptionsBuilder
    {
        private readonly BasicGridDataRowBuilder gridDataRowBuilder;

        public GridOptionsBuilderImp(BasicGridDataRowBuilder gridDataRowBuilder)
        {
            this.gridDataRowBuilder = gridDataRowBuilder;
        }

        public GridOptions Build(ProductWinLoss winLoss, string title)
        {
            return new GridOptions
            {
                CustomizeHeader = true,
                ShowTitle = true,
                TitleType = TitleType.TwoLineBlue,
                TitleInfoText = title.ToUpperInvariant(),
                BodyRows = winLoss.IsEmpty ? new List<GridDataRow>() : BuildBodyRows(winLoss),
                FooterRows = BuildFooter(winLoss.GrandTotal, winLoss.GrandTotalWithoutCN88),
                ShowHighlightRow = false,
                ShowDistinctRow = false,
                ShowFooter = true,
                ShowTree = true
            };
        }

        private List<GridDataRow> BuildBodyRows(ProductWinLoss productWinLoss)
        {
            var sportsbookRows = BuildProductGroup(productWinLoss.Sportsbook);
            var sportsbookRMRows = BuildSportsbookRmRows(productWinLoss.RM);
            var nonSportsbookRows = BuildProductGroup(productWinLoss.NonSportsbook);

            var bodyRows = new List<GridDataRow>();
            bodyRows.AddRange(sportsbookRows);
            bodyRows.AddRange(sportsbookRMRows);
            if (sportsbookRows.Any() || sportsbookRMRows.Any())
            {
                bodyRows.Add(EmptyRow());
            }

            bodyRows.AddRange(nonSportsbookRows);

            if (nonSportsbookRows.Any())
            {
                bodyRows.Add(EmptyRow());
            }

            return bodyRows;
        }

        private IEnumerable<GridDataRow> BuildProductGroup(ProductGroupWinLoss productGroupWinLoss)
        {
            var bodyRows = productGroupWinLoss
                .Products
                .Select(BuildSiteWinLossSummary)
                .ToList();

            var result = new List<GridDataRow>(bodyRows);
            if (bodyRows.Any())
            {
                var totalRow = BuildSiteWinLossSummaryForTotalRow(productGroupWinLoss.Total);
                result.Add(totalRow);
            }

            return result;
        }

        private GridDataRow EmptyRow()
        {
            var colummns = new List<Columns>
            {
                new Columns { ColumnIndex = 1, Name = "Main", Value = string.Empty, MergeColNumber = 13 }
            };

            return new GridDataRow { Columns = colummns, RowType = RowType.Normal, CssClass = string.Empty };
        }

        private GridDataRow BuildSiteWinLossSummary(SiteWinLossSummary winloss)
        {
            var productDataRow = gridDataRowBuilder.BuildDataRow(winloss.Total, HAlignment.Left);
            productDataRow.RowType = RowType.ParentLevelOne;
            productDataRow.CustomAttr = "product";

            var creditRow = gridDataRowBuilder.BuildDataRow(winloss.Credit, HAlignment.Right);
            creditRow.RowType = RowType.Normal;
            productDataRow.SubRows.Add(creditRow);

            var depositRow = gridDataRowBuilder.BuildDataRow(winloss.Deposit, HAlignment.Right);
            depositRow.RowType = RowType.Normal;
            productDataRow.SubRows.Add(depositRow);

            var cn88Row = gridDataRowBuilder.BuildDataRow(winloss.CN88, HAlignment.Right);
            cn88Row.RowType = RowType.Normal;
            productDataRow.SubRows.Add(cn88Row);

            return productDataRow;
        }

        private GridDataRow BuildSiteWinLossSummaryForTotalRow(SiteWinLossSummary productWinLoss)
        {
            const string RowCssClass = "total-row-main";
            var productDataRow = gridDataRowBuilder.BuildDataRow(productWinLoss.Total, HAlignment.Right);
            productDataRow.CssClass = RowCssClass;
            productDataRow.RowType = RowType.SubTotal;
            productDataRow.CustomAttr = null;

            var creditRow = gridDataRowBuilder.BuildDataRow(productWinLoss.Credit, HAlignment.Right);
            creditRow.CssClass = RowCssClass;
            creditRow.RowType = RowType.SubTotal;
            productDataRow.SubRows.Add(creditRow);

            var depositRow = gridDataRowBuilder.BuildDataRow(productWinLoss.Deposit, HAlignment.Right);
            depositRow.CssClass = RowCssClass;
            depositRow.RowType = RowType.SubTotal;
            productDataRow.SubRows.Add(depositRow);

            var cn88Row = gridDataRowBuilder.BuildDataRow(productWinLoss.CN88, HAlignment.Right);
            cn88Row.CssClass = RowCssClass;
            cn88Row.RowType = RowType.SubTotal;
            productDataRow.SubRows.Add(cn88Row);

            return productDataRow;
        }

        private IEnumerable<GridDataRow> BuildSportsbookRmRows(IEnumerable<WinLoss> winLosses)
        {
            var rows = winLosses
                .Select(w => gridDataRowBuilder.BuildDataRow(w, HAlignment.Right))
                .ToList();
            rows.ForEach(r =>
            {
                r.RowType = RowType.SubTotal;
                r.CssClass = "total-row-main";
            });

            return rows;
        }

        private List<GridDataRow> BuildFooter(SiteWinLossSummary item, WinLoss cn88Total)
        {
            var row = gridDataRowBuilder.BuildDataRow(item.Total, HAlignment.Right);
            var creditRow = gridDataRowBuilder.BuildDataRow(item.Credit, HAlignment.Right);
            var depositRow = gridDataRowBuilder.BuildDataRow(item.Deposit, HAlignment.Right);
            var cn88Row = gridDataRowBuilder.BuildDataRow(item.CN88, HAlignment.Right);

            FormatTotalRow(row);
            FormatTotalRow(creditRow);
            FormatTotalRow(depositRow);
            FormatTotalRow(cn88Row);

            row.SubRows.Add(creditRow);
            row.SubRows.Add(depositRow);
            row.SubRows.Add(cn88Row);

            var cn88TotalRow = gridDataRowBuilder.BuildDataRow(cn88Total, HAlignment.Right);
            FormatTotalRow(cn88TotalRow);

            return new List<GridDataRow> { row, cn88TotalRow };
        }

        private static void FormatTotalRow(GridDataRow row)
        {
            row.CssClass = "grand-total";
            row.Columns.First(c => c.Name == "VirtualMargin").Value = string.Empty;
            row.Columns.First(c => c.Name == "RealMargin").Value = string.Empty;
        }
    }
}