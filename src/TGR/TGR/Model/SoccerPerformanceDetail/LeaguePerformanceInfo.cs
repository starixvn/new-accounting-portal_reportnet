namespace TGR.Model.SoccerPerformanceDetail;

public class LeaguePerformanceInfo
{
    public int? LeagueId { get; set; }

    public string LeagueName { get; set; }

    public short? LeagueGroupID { get; set; }

    public string LeagueGroupName { get; set; }

    public decimal? LeagueBenchmark { get; set; }

    public decimal? BetTypeBMShift1 { get; set; }

    public decimal? BetTypeBMShift2 { get; set; }

    public decimal? BetTypeBMShift3 { get; set; }

    public int? NoOfTraders { get; set; }

    public int? NoOfMatches { get; set; }

    public int? NoOfBetChoices { get; set; }

    public decimal? Turnover { get; set; }

    public decimal? WinLoss { get; set; }

    public decimal? Margin { get; set; }

    public decimal? TurnoverReal { get; set; }

    public decimal? WinLossReal { get; set; }

    public decimal? MarginReal { get; set; }

    public int? BetCount { get; set; }

    public Market Market { get; set; }

    public bool IsLive { get; set; }

    public int FlagForViewReport { get; set; }
}