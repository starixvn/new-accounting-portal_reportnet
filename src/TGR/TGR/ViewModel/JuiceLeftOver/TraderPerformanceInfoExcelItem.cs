using Epplus.Mapper.Annotations;

namespace TGR.ViewModel.JuiceLeftOver;

public class TraderPerformanceInfoExcelItem : BaseExcelItem
{
    [Cell("B5")]
    public string Username { get; set; }

    [Cell("C5")]
    public byte Grade { get; set; }

    [Cell("D5")]
    public string TeamName { get; set; }

    [Cell("E5")]
    public int LeagueCount { get; set; }

    [Cell("F5")]
    public int MatchCount { get; set; }

    [Cell("G5")]
    public decimal TurnoverJuice { get; set; }

    [Cell("H5")]
    public decimal Juice { get; set; }

    [Cell("I5")]
    public decimal? JuiceMargin { get; set; }

    [Cell("J5")]
    public decimal? JuiceMarginTotal { get; set; }

    [Cell("K5")]
    public decimal TurnoverLeftover { get; set; }

    [Cell("L5")]
    public decimal? WinlostLeftover { get; set; }

    [Cell("M5")]
    public decimal? LeftoverMargin { get; set; }

    [Cell("N5")]
    public decimal LeftoverMarginTotal { get; set; }

    [Cell("O5")]
    public decimal Turnover { get; set; }

    [Cell("P5")]
    public decimal WinLoss { get; set; }

    [Cell("Q5")]
    public decimal? Margin { get; set; }

    [Cell("R5")]
    public int BetCount { get; set; }

    [Cell("S5")]
    public decimal? Score { get; set; }
}