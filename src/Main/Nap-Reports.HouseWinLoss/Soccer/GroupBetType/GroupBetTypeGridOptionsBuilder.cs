using System.Collections.Generic;
using System.Linq;
using Fanex.Excel.Enums;
using Fanex.Excel.Models;
using Nap_Reports.HouseWinLoss.Common.BasicWinLoss;
using Nap_Reports.HouseWinLoss.Soccer.BaseService;

namespace Nap_Reports.HouseWinLoss.Soccer.GroupBetType
{
    public interface GroupBetTypeGridOptionsBuilder
    {
        GridOptions Build(GroupBetTypeWinLossModel winLoss, string title);
    }

    public class GroupBetTypeGridOptionsBuilderImp : BaseBetTypeGridOptionsBuilder, GroupBetTypeGridOptionsBuilder
    {
        public GroupBetTypeGridOptionsBuilderImp(BasicGridDataRowBuilder gridDataRowBuilder)
            : base(gridDataRowBuilder)
        {
        }

        public GridOptions Build(GroupBetTypeWinLossModel winLoss, string title)
        {
            return new GridOptions
            {
                CustomizeHeader = true,
                ShowTitle = true,
                TitleType = TitleType.TwoLineBlue,
                TitleInfoText = title.ToUpperInvariant(),
                BodyRows = BuildGroupBetTypeBodyRows(winLoss.SiteWinLossSummaries),
                FooterRows = BuildFooter(winLoss),
                ShowFooter = true,
                ShowTree = true,
                ShowHighlightRow = false,
                ShowDistinctRow = false
            };
        }

        private List<GridDataRow> BuildGroupBetTypeBodyRows(IEnumerable<WinLossSummaryByBetTypeGroup> winLoss)
        {
            var bodyRows = new List<GridDataRow>();
            foreach (var winLossSummary in winLoss)
            {
                var rows = winLossSummary
                    .SiteWinLossSummaries
                    .Select(item => BuildBetTypeDataRow(item, HAlignment.Left));
                var totalRow = GridDataRowBuilder.BuildDataRow(winLossSummary.Total, HAlignment.Right);
                totalRow.RowType = RowType.SubTotal;
                totalRow.CssClass = "total-row";
                bodyRows.AddRange(rows);
                bodyRows.Add(totalRow);
            }

            return bodyRows;
        }
    }
}