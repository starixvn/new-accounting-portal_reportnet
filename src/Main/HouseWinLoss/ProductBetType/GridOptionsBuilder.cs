using Fanex.Excel.Enums;
using Fanex.Excel.Models;
using HouseWinLoss.Common.BasicWinLoss;
using HouseWinLoss.Common.Models;

namespace HouseWinLoss.ProductBetType
{
    public interface GridOptionsBuilder
    {
        GridOptions Build(ProductBetTypeWinLoss winLoss, string title);
    }

    public class GridOptionsBuilderImp(BasicGridDataRowBuilder gridDataRowBuilder) : GridOptionsBuilder
    {
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
