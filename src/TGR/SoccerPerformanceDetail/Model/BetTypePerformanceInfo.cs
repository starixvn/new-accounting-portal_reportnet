namespace SoccerPerformanceDetail.Model;

public class BetTypePerformanceInfo
{
    public short? BetTypeId { get; set; }

    public string BetTypeName { get; set; }

    public int? NoOfTraders { get; set; }

    public int? NoOfLeagues { get; set; }

    public int? NoOfMatches { get; set; }

    public decimal? Turnover { get; set; }

    public decimal? WinLoss { get; set; }

    public int? BetCount { get; set; }

    public decimal? Margin { get; set; }

    public decimal? TurnoverReal { get; set; }

    public decimal? WinLossReal { get; set; }

    public decimal? MarginReal { get; set; }

    public bool IsLive { get; set; }

    public int FlagForViewReport { get; set; }
}