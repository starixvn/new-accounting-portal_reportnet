using Epplus.Mapper.Annotations;

namespace TGR.ViewModel.SoccerPerformanceDetail;

public class LeaguePerformanceExcelItem : BasePerformanceExcelItem
{
    [Cell("B5")]
    public string LeagueName { get; set; }

    [Cell("D5")]
    public string LeagueGroupName { get; set; }

    [Cell("E5")]
    public int NoOfTraders { get; set; }

    [Cell("G5")]
    public int BetCount { get; set; }

    [Cell("I5")]
    public decimal? AvgToPerMatch { get; set; }

    [Cell("J5")]
    public decimal WinLoss { get; set; }

    [Cell("M5")]
    public decimal? AvgToPerMatchReal { get; set; }

    [Cell("N5")]
    public decimal WinLossReal { get; set; }
}

public class LiveLeaguePerformanceExcelItem
{
    [Cell("A5")]
    public int No { get; set; }

    [Cell("B5")]
    public string LeagueName { get; set; }

    [Cell("C5")]
    public string LeagueGroupName { get; set; }

    [Cell("D5")]
    public int NoOfMatches { get; set; }

    [Cell("E5")]
    public int BetCount { get; set; }

    [Cell("F5")]
    public decimal Turnover { get; set; }

    [Cell("G5")]
    public decimal? AvgToPerMatch { get; set; }

    [Cell("H5")]
    public decimal WinLoss { get; set; }

    [Cell("I5")]
    public decimal? Margin { get; set; }

    [Cell("J5")]
    public decimal TurnoverReal { get; set; }

    [Cell("K5")]
    public decimal? AvgToPerMatchReal { get; set; }

    [Cell("L5")]
    public decimal WinLossReal { get; set; }

    [Cell("M5")]
    public decimal? MarginReal { get; set; }

    public bool IsLive { get; set; }
}