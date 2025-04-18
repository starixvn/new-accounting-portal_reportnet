namespace HouseWinLoss.Common.SiteWinLoss;

using BasicWinLoss;
using Fanex.Excel.Enums;
using Fanex.Excel.Models;
using Models;

public interface GridOptionsBuilder
{
    GridOptions Build(SiteWinLossModel winLoss, string title);
}

public class GridOptionsBuilderImp(BasicGridDataRowBuilder gridDataRowBuilder) : GridOptionsBuilder
{
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
        return [row];
    }
}