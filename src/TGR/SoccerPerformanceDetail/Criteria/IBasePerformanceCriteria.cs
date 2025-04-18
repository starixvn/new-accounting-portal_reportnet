using SoccerPerformanceDetail;

public interface IBasePerformanceCriteria
{
    Market MarketFilter { get; set; }
    DateTime FromDate { get; set; }
    DateTime ToDate { get; set; }
    string BetTypes { get; set; }
    string LeagueGroupIds { get; set; }
    
    bool IsVirtual { get; set; }
    decimal? Turnover { get; set; }
    decimal? WinLoss { get; set; }
    decimal? MarginFrom { get; set; }
    decimal? MarginTo { get; set; }

    bool IsReal { get; set; }
    decimal? TurnoverReal { get; set; }
    decimal? WinLossReal { get; set; }
    decimal? MarginFromReal { get; set; }
    decimal? MarginToReal { get; set; }
} 