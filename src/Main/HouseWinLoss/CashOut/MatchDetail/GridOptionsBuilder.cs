using Fanex.Excel.Enums;
using Fanex.Excel.Models;
using HouseWinLoss.Common.MatchDetail;

namespace HouseWinLoss.CashOut.MatchDetail
{
    public class GridOptionsBuilderImp : GridOptionsBuilder
    {
        public GridOptions Build(WinLossModel winLoss, string title)
        {
            var gridOptions = new GridOptions
            {
                ShowTitle = true,
                TitleType = TitleType.TwoLineBlue,
                TitleInfoText = title.ToUpperInvariant(),
                BodyRows = BuildBodyRows(winLoss.Leagues),
                FooterRows = BuildFooter(winLoss.Total),
                ShowFooter = true,
                ShowDistinctRow = false,
                TotalItems = winLoss.TotalRows
            };

            return gridOptions;
        }

        private List<GridDataRow> BuildBodyRows(IEnumerable<LeagueWinLoss> leagues)
        {
            return leagues
                .Select(BuildLeagues)
                .SelectMany(t => t)
                .ToList();
        }

        private List<GridDataRow> BuildLeagues(LeagueWinLoss league)
        {
            var leagueNameRow = BuildLeagueNameRow(league.LeagueName);
            var matchRows = league.Matches.Select(m => BuildDataRow(m, HAlignment.Center));
            league.Total.LeagueName = string.IsNullOrEmpty(league.Total.LeagueName) ? "Total" : league.Total.LeagueName;
            var totalRows = BuildDataRow(league.Total, HAlignment.Right);
            totalRows.CssClass = "total-row";
            totalRows.RowType = RowType.SubTotal;

            var rows = new List<GridDataRow> { leagueNameRow };
            rows.AddRange(matchRows);
            rows.Add(totalRows);

            return rows;
        }

        private GridDataRow BuildDataRow(WinLoss item, HAlignment alignment)
        {
            var columnIndex = 0;
            var columns = new List<Columns>
            {
                new Columns(columnIndex++, "MatchCode", item.MatchCode, alignment),
                new Columns(columnIndex++, "MemberWinLoss", item.MemberWinLoss),
                new Columns(columnIndex++, "SMAWinLoss", item.SMAWinLoss),
                new Columns(columnIndex++, "ExtraPT", item.ExtraPT),
                new Columns(columnIndex++, "Commission", item.Commission),
                new Columns(columnIndex++, "RealIncome", item.RealIncome),
                new Columns(columnIndex++, "RealTurnover", item.RealTurnover),
                new Columns(columnIndex, "RealMargin", item.RealTurnover == 0 ? "N/A" : (object)item.RealMargin)
            };

            return new GridDataRow { Columns = columns };
        }

        private GridDataRow BuildLeagueNameRow(string leagueName)
        {
            var columnIndex = 0;
            var colummns = new List<Columns>
               {
                    new Columns(columnIndex++, "MatchCode", string.Empty),
                    new Columns { ColumnIndex = columnIndex, Name = "MemberWinLoss", Value = leagueName, MergeColNumber = 7, Bold = true, CellAlignment = HAlignment.Left }
               };

            return new GridDataRow { Columns = colummns, RowType = RowType.ParentLevelOne, CssClass = "league-match-header" };
        }

        private List<GridDataRow> BuildFooter(WinLoss item)
        {
            return new List<GridDataRow> { BuildDataRow(item, HAlignment.Right) };
        }
    }
}