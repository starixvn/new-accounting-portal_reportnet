using Fanex.Excel.Enums;
using Fanex.Excel.Models;
using HouseWinLoss.Common.BasicWinLoss;
using HouseWinLoss.Common.Models;
using HouseWinLoss.Soccer.BaseService;

namespace HouseWinLoss.Soccer.SumBetType;

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