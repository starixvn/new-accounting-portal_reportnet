using Fanex.Excel.Enums;
using Fanex.Excel.Models;
using HouseWinLoss.Common.Models;

namespace HouseWinLoss.Common.BasicWinLoss;

public interface BasicGridOptionsBuilder
{
    GridOptions Build(BasicWinLossModel winLoss, string title);
}

public class BasicGridOptionsBuilderImp(BasicGridDataRowBuilder gridDataRowBuilder) : BasicGridOptionsBuilder
{
    private const string grandTotalClass = "grand-total";

    public GridOptions Build(BasicWinLossModel winLoss, string title)
    {
        return new GridOptions
        {
            ShowTitle = true,
            TitleType = TitleType.TwoLineBlue,
            TitleInfoText = title.ToUpperInvariant(),
            BodyRows = BuildBodyRows(winLoss.SiteWinLossSummaries),
            FooterRows = BuildFooter(winLoss),
            ShowFooter = true,
            ShowTree = true,
            ShowHighlightRow = false,
            ShowDistinctRow = false
        };
    }

    private List<GridDataRow> BuildBodyRows(IEnumerable<SiteWinLossSummary> winLoss)
    {
        return winLoss.Select(item => BuildGroupDataRow(item, HAlignment.Left)).ToList();
    }

    private List<GridDataRow> BuildFooter(BasicWinLossModel model)
    {
        var row = BuildTotalGroupDataRow(model.GrandTotal);
        var allRMRows = model.RM.Select(r => gridDataRowBuilder.BuildDataRow(r, HAlignment.Right));

        var footer = new List<GridDataRow> { row };
        footer.AddRange(allRMRows);

        return footer;
    }

    protected virtual GridDataRow BuildGroupDataRow(SiteWinLossSummary item, HAlignment alignment)
    {
        var row = gridDataRowBuilder.BuildDataRow(item.Total, alignment);
        row.RowType = RowType.ParentLevelOne;

        var creditRow = gridDataRowBuilder.BuildDataRow(item.Credit, HAlignment.Right);
        var depositRow = gridDataRowBuilder.BuildDataRow(item.Deposit, HAlignment.Right);
        var cn88Row = gridDataRowBuilder.BuildDataRow(item.CN88, HAlignment.Right);

        row.SubRows.Add(creditRow);
        row.SubRows.Add(depositRow);
        row.SubRows.Add(cn88Row);

        return row;
    }

    private GridDataRow BuildTotalGroupDataRow(SiteWinLossSummary item)
    {
        var row = gridDataRowBuilder.BuildDataRow(item.Total, HAlignment.Right);
        row.RowType = RowType.ParentLevelOne;
        row.CssClass = grandTotalClass;

        var creditRow = gridDataRowBuilder.BuildDataRow(item.Credit, HAlignment.Right);
        creditRow.CssClass = grandTotalClass;

        var depositRow = gridDataRowBuilder.BuildDataRow(item.Deposit, HAlignment.Right);
        depositRow.CssClass = grandTotalClass;

        var cn88Row = gridDataRowBuilder.BuildDataRow(item.CN88, HAlignment.Right);
        cn88Row.CssClass = grandTotalClass;

        row.SubRows.Add(creditRow);
        row.SubRows.Add(depositRow);
        row.SubRows.Add(cn88Row);

        return row;
    }
}