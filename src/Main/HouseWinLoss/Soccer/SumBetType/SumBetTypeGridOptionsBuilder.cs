namespace HouseWinLoss.Soccer.SumBetType;

using BaseService;
using Common.BasicWinLoss;
using Common.Models;
using Fanex.Excel.Enums;
using Fanex.Excel.Models;

public interface SumBetTypeGridOptionsBuilder
{
    GridOptions Build(SumBetTypeWinLossModel winLoss, string title);
}

public class SumBetTypeGridOptionsBuilderImp(BasicGridDataRowBuilder gridDataRowBuilder)
    : BaseBetTypeGridOptionsBuilder(gridDataRowBuilder), SumBetTypeGridOptionsBuilder
{
    public GridOptions Build(SumBetTypeWinLossModel winLoss, string title)
    {
        return new GridOptions
        {
            CustomizeHeader = true,
            ShowTitle = true,
            TitleType = TitleType.TwoLineBlue,
            TitleInfoText = title.ToUpperInvariant(),
            BodyRows = BuildSumBetTypeBodyRows(winLoss.SiteWinLossSummaries),
            FooterRows = BuildFooter(winLoss),
            ShowFooter = true,
            ShowTree = true,
            ShowHighlightRow = false,
            ShowDistinctRow = false
        };
    }

    private List<GridDataRow> BuildSumBetTypeBodyRows(IEnumerable<SiteWinLossSummary> winLoss)
    {
        return winLoss
            .Select(item => BuildBetTypeDataRow(item, HAlignment.Left))
            .ToList();
    }
}