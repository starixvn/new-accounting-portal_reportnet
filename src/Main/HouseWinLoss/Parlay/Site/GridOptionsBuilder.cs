using Fanex.Excel.Enums;
using Fanex.Excel.Models;
using HouseWinLoss.Common.BasicWinLoss;
using HouseWinLoss.Common.Models;

namespace HouseWinLoss.Parlay.Site
{
    public interface GridOptionsBuilder
    {
        GridOptions Build(SiteParlayModel winLoss, string title);
    }

    public class GridOptionsBuilderImp : GridOptionsBuilder
    {
        private readonly BasicGridDataRowBuilder gridDataRowBuilder;

        public GridOptionsBuilderImp(BasicGridDataRowBuilder gridDataRowBuilder)
        {
            this.gridDataRowBuilder = gridDataRowBuilder;
        }

        public GridOptions Build(SiteParlayModel winLoss, string title)
        {
            return new GridOptions
            {
                ShowTitle = true,
                TitleInfoText = title.ToUpperInvariant(),
                TitleType = TitleType.TwoLineBlue,
                BodyRows = BuildSites(winLoss.Sites),
                FooterRows = BuildFooter(winLoss.Total),
                ShowFooter = true,
                ShowHighlightRow = false,
                ShowDistinctRow = false
            };
        }

        private List<GridDataRow> BuildSites(IEnumerable<SiteModel> sites)
        {
            var sitesResult = new List<GridDataRow>();
            foreach (var group in sites)
            {
                sitesResult.AddRange(BuildBodyRows(group.Sites).ToList());
                var total = gridDataRowBuilder.BuildDataRow(group.Total, HAlignment.Center);
                total.CssClass = "total-row";
                sitesResult.Add(total);
                sitesResult.Add(EmptyRow());
            }

            return sitesResult;
        }

        private GridDataRow EmptyRow()
        {
            var colummns = new List<Columns>
            {
                new Columns { ColumnIndex = 1, Name = "Main", Value = string.Empty, MergeColNumber = 13 }
            };

            return new GridDataRow { Columns = colummns, RowType = RowType.Normal, CssClass = string.Empty };
        }

        private IEnumerable<GridDataRow> BuildBodyRows(IEnumerable<WinLoss> winLoss)
        {
            return winLoss.Select(item => gridDataRowBuilder.BuildDataRow(item, HAlignment.Center));
        }

        private List<GridDataRow> BuildFooter(WinLoss item)
        {
            return new List<GridDataRow> { gridDataRowBuilder.BuildDataRow(item, HAlignment.Right) };
        }
    }
}