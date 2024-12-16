using Fanex.Excel.Enums;
using Fanex.Excel.Models;
using HouseWinLoss.Common.BasicWinLoss;
using HouseWinLoss.Soccer.BaseService;

namespace HouseWinLoss.Soccer.GroupBetType;

public interface GroupBetTypeGridOptionsBuilder
{
    GridOptions Build(GroupBetTypeWinLossModel winLoss, string title);
}

public class GroupBetTypeGridOptionsBuilderImp(BasicGridDataRowBuilder gridDataRowBuilder)
    : BaseBetTypeGridOptionsBuilder(gridDataRowBuilder), GroupBetTypeGridOptionsBuilder
{
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