using Epplus.Mapper.Annotations;

namespace SoccerPerformanceDetail.ViewModel;

public class BetTypePerformanceExcelItem
{
    [Cell("A5")]
    public int No { get; set; }

    [Cell("B5")]
    public string BetTypeName { get; set; }

    [Cell("C5")]
    public string Market { get; set; }

    [Cell("D5")]
    public int NoOfTraders { get; set; }

    [Cell("E5")]
    public int NoOfLeagues { get; set; }

    [Cell("F5")]
    public int NoOfMatches { get; set; }

    [Cell("G5")]
    public int BetCount { get; set; }

    [Cell("H5")]
    public decimal Turnover { get; set; }

    [Cell("I5")] public decimal AvgToPerMatch => NoOfMatches > 0 ? Turnover / NoOfMatches : 0;

    [Cell("J5")]
    public decimal WinLoss { get; set; }

    [Cell("K5")]
    public decimal? Margin { get; set; }

    [Cell("L5")]
    public decimal TurnoverReal { get; set; }

    [Cell("M5")] public decimal AvgToPerMatchReal => NoOfMatches > 0 ? TurnoverReal / NoOfMatches : 0;

    [Cell("N5")]
    public decimal WinLossReal { get; set; }

    [Cell("O5")]
    public decimal? MarginReal { get; set; }

    public bool IsLive { get; set; }
}