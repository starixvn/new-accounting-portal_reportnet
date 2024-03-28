using Fanex.Excel.Enums;
using Fanex.Excel.Models;
using System.Collections.Generic;
using System.Linq;
using Nap_Reports.HouseWinLoss.Common.BasicWinLoss;
using Nap_Reports.HouseWinLoss.Common.Models;

namespace Nap_Reports.HouseWinLoss.ProductBetType
{
    public interface GridOptionsBuilder
    {
        GridOptions Build(ProductBetTypeWinLoss winLoss, string title);
    }

    public class GridOptionsBuilderImp : GridOptionsBuilder
    {
        private readonly BasicGridDataRowBuilder gridDataRowBuilder;

        public GridOptionsBuilderImp(BasicGridDataRowBuilder gridDataRowBuilder)
        {
            this.gridDataRowBuilder = gridDataRowBuilder;
        }
        public GridOptions Build(ProductBetTypeWinLoss winLoss, string title)
        {
            var bodyRows = BuildBodyRows(winLoss);

            return new GridOptions
            {
                CustomizeHeader = true,
                ShowTitle = true,
                TitleType = TitleType.TwoLineBlue,
                TitleInfoText = title.ToUpperInvariant(),
                BodyRows = bodyRows.ToList(),
                FooterRows = BuildFooter(winLoss.GrandTotal),
                ShowHighlightRow = false,
                ShowDistinctRow = false,
                ShowFooter = true,
                ShowTree = true
            };
        }

        private IEnumerable<GridDataRow> BuildBodyRows(ProductBetTypeWinLoss winLosses)
        {
            return from betTypeWinLoss in winLosses.BetTypes select gridDataRowBuilder.BuildDataRow(betTypeWinLoss, HAlignment.Left);
        }

        private List<GridDataRow> BuildFooter(WinLoss winLosses)
        {
            var result = new List<GridDataRow>();
            var row = gridDataRowBuilder.BuildDataRow(winLosses, HAlignment.Center);
            result.Add(row);

            return result;
        }
    }
}
