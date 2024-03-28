using System.Collections.Generic;
using System.Linq;
using Fanex.Excel.Enums;
using Fanex.Excel.Models;
using Nap_Reports.HouseWinLoss.Common.BasicWinLoss;
using Nap_Reports.HouseWinLoss.Common.Models;

namespace Nap_Reports.HouseWinLoss.Common.SiteWinLoss
{
    public interface GridOptionsBuilder
    {
        GridOptions Build(SiteWinLossModel winLoss, string title);
    }

    public class GridOptionsBuilderImp : GridOptionsBuilder
    {
        private readonly BasicGridDataRowBuilder gridDataRowBuilder;

        public GridOptionsBuilderImp(BasicGridDataRowBuilder gridDataRowBuilder)
        {
            this.gridDataRowBuilder = gridDataRowBuilder;
        }

        public GridOptions Build(SiteWinLossModel winLoss, string title)
        {
            return new GridOptions
            {
                ShowTitle = true,
                TitleInfoText = title.ToUpperInvariant(),
                TitleType = TitleType.TwoLineBlue,
                BodyRows = BuildBodyRows(winLoss.Sites),
                FooterRows = BuildFooter(winLoss.Total),
                ShowFooter = true
            };
        }

        private List<GridDataRow> BuildBodyRows(IEnumerable<WinLoss> winLoss)
        {
            return winLoss
                .Select(item => gridDataRowBuilder.BuildDataRow(item, HAlignment.Center))
                .ToList();
        }

        private List<GridDataRow> BuildFooter(WinLoss item)
        {
            var row = gridDataRowBuilder.BuildDataRow(item, HAlignment.Center);
            return new List<GridDataRow> { row };
        }
    }
}