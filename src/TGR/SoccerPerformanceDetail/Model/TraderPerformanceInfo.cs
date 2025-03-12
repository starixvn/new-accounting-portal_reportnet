
namespace SoccerPerformanceDetail.Model;

public class TraderPerformanceInfo
{
    public int? TraderId { get; set; }

    public string TraderName { get; set; }

    public byte? Grade { get; set; }

    public short? TeamId { get; set; }

    public string TeamName { get; set; }

    public int? NoOfLeagues { get; set; }

    public int? NoOfMatches { get; set; }

    public int? BetCount { get; set; }

    public decimal? Turnover { get; set; }

    public decimal? WinLoss { get; set; }       

    public decimal? Margin { get; set; }

    public decimal? TurnoverReal { get; set; }

    public decimal? WinLossReal { get; set; }

    public decimal? MarginReal { get; set; }

    public int? NoOfTeamChanged { get; set; }

    public int? NoOfGradeChanged { get; set; }

    public decimal? Score { get; set; }

    public bool IsLive { get; set; }
}