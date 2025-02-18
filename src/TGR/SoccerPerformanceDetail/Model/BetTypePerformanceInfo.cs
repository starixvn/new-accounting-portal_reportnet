namespace SoccerPerformanceDetail.Model;

public class BetTypePerformanceInfo
{
    public short? BetType { get; set; }

    public string TypeName { get; set; }

    public int? TraderCount { get; set; }

    public int? LeagueCount { get; set; }

    public int? MatchCount { get; set; }

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