using Fanex.Excel.Enums;
using Fanex.Excel.Models;
using HouseWinLoss.Common.Extensions;

namespace HouseWinLoss.Common.MatchDetail;

public interface GridOptionsBuilder
{
    GridOptions Build(WinLossModel winLoss, string title);
}

public class GridOptionsBuilderImp(GetTeamNameAndSuffixService teamNameService) : GridOptionsBuilder
{
    public GridOptions Build(WinLossModel winLoss, string title)
    {
        var gridOptions = new GridOptions
        {
            ShowTitle = true,
            TitleInfoText = title.ToUpperInvariant(),
            TitleType = TitleType.TwoLineBlue,
            BodyRows = BuildBodyRows(winLoss.Leagues),
            FooterRows = BuildFooter(winLoss),
            ShowFooter = true,
            ShowDistinctRow = false,
            TotalItems = winLoss.TotalRows
        };

        return gridOptions;
    }

    private List<GridDataRow> BuildBodyRows(IEnumerable<LeagueWinLoss> leagueWinLoss)
    {
        var gridDataRows = leagueWinLoss.Select(BuildLeagueDataRows).ToList();

        return gridDataRows.SelectMany(x => x).ToList();
    }

    private List<GridDataRow> BuildLeagueDataRows(LeagueWinLoss league)
    {
        var dataRows = new List<GridDataRow>();
        var leagueNameRow = BuildLeagueNameRow(league.LeagueName);
        var matchRows = league.Matches.Select(m => BuildMatchRow(m, league.LeagueName));

        dataRows.Add(leagueNameRow);
        dataRows.AddRange(matchRows);

        var leagueTotalRow = BuildTotalRow(league.Total, "Total");
        leagueTotalRow.CssClass = "total-row";
        leagueTotalRow.RowType = RowType.SubTotal;
        dataRows.Add(leagueTotalRow);

        return dataRows;
    }

    private GridDataRow BuildLeagueNameRow(string leagueName)
    {
        var columnIndex = 0;
        var columns = new List<Columns>
        {
            new(columnIndex++, "MatchCode", string.Empty),
            new()
            {
                ColumnIndex = columnIndex,
                Name = "Home",
                Value = leagueName,
                MergeColNumber = 14,
                Bold = true,
                CellAlignment = HAlignment.Left
            }
        };

        return new GridDataRow
        {
            Columns = columns,
            RowType = RowType.ParentLevelOne,
            CssClass = "league-match-header"
        };
    }

    private GridDataRow BuildMatchRow(WinLoss item, string leagueName)
    {
        var homeName = teamNameService.GetTeamNameWithSuffixByMatchCode(item.HomeName, item.MatchCode);
        var awayName = teamNameService.GetTeamNameWithSuffixByMatchCode(item.AwayName, item.MatchCode);

        var columnIndex = 0;
        var customAttributes = $"{item.SportType},{item.MatchId},'{item.EventDate:MMM dd, yyyy}','{leagueName}','{item.MatchCode}'";
        var columns = new List<Columns>
        {
            new(0, "MatchCode", item.MatchCode)
            {
                CustomAttributes = customAttributes,
                Bold = true,
                CssClass = "league-match-code",
                CellAlignment = HAlignment.Center
            },
            new(columnIndex++, "Home", homeName),
            new(columnIndex++, "VS", "-vs-") { CellAlignment = HAlignment.Center },
            new(columnIndex++, "Away", awayName) { CellAlignment = HAlignment.Left }
        };

        var baseColumns = CreateBaseColumns(columnIndex, item);
        columns.AddRange(baseColumns);

        return new GridDataRow
        {
            Columns = columns,
            RowType = RowType.Normal
        };
    }

    private List<GridDataRow> BuildFooter(WinLossModel model)
    {
        var totalRow = BuildTotalRow(model.Total, "Grand Total");
        var allRMRows = model.RM.Select(r => BuildTotalRow(r, r.MatchCode));
        var result = new List<GridDataRow> { totalRow };
        result.AddRange(allRMRows);

        return result;
    }

    private GridDataRow BuildTotalRow(WinLoss item, string totalText)
    {
        const int BaseColumnIndex = 4;
        var columns = new List<Columns>
        {
            new()
            {
                ColumnIndex = 0,
                Name = "MatchCode",
                Value = totalText,
                MergeColNumber = 4,
                CellAlignment = HAlignment.Right
            }
        };

        var baseColumns = CreateBaseColumns(BaseColumnIndex, item);
        columns.AddRange(baseColumns);

        return new GridDataRow { Columns = columns };
    }

    private static List<Columns> CreateBaseColumns(int columnIndex, WinLoss item)
    {
        return
        [
            new Columns(columnIndex++, "MemberWinLoss", item.MemberWinLoss),
            new Columns(columnIndex++, "SMAWinLoss", item.SMAWinLoss),
            new Columns(columnIndex++, "SMAWinLoss", item.ExtraPT),
            new Columns(columnIndex++, "Commission", item.Commission),
            new Columns(columnIndex++, "VarianceIncome", item.Variance),
            new Columns(columnIndex++, "VirtualIncome", item.VirtualIncome),
            new Columns(columnIndex++, "RealIncome", item.RealIncome),
            new Columns(columnIndex++, "VirtualTurnover", item.VirtualTurnover),
            new Columns(columnIndex++, "RealTurnover", item.RealTurnover),
            new Columns(columnIndex++, "VirtualMargin", item.ExcelVirtualMargin()),
            new Columns(columnIndex, "RealMargin", item.ExcelRealMargin())
        ];
    }
}