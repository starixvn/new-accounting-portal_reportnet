namespace SoccerPerformanceDetail.ViewModel;

using Epplus.Mapper.Annotations;

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