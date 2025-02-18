
namespace SoccerPerformanceDetail.Model;


public class MatchPerformanceRawInfo
{
    public int? LeagueId { get; set; }

    public int? MatchId { get; set; }

    public int? HomeId { get; set; }

    public int? AwayId { get; set; }

    public DateTime? EventDate { get; set; }

    public int? BetCount { get; set; }

    public decimal? Turnover { get; set; }

    public decimal? WinLoss { get; set; }

    public decimal? WinLossScore { get; set; }        

    public decimal? Margin { get; set; }

    public decimal? TurnoverReal { get; set; }

    public decimal? WinLossReal { get; set; }

    public decimal? MarginReal { get; set; }

    public decimal? SumCategory { get; set; }
}