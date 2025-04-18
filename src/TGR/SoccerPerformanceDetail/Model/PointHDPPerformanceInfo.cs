namespace SoccerPerformanceDetail.Model;

public class PointHDPPerformanceInfo
{
    public decimal PointHDP { get; set; }

    public int NoOfLeagues { get; set; }

    public int NoOfMatches { get; set; }

    public int BetCount { get; set; }

    public decimal Turnover { get; set; }

    public decimal WinLoss { get; set; }

    public decimal? Margin { get; set; }        

    public decimal TurnoverReal { get; set; }

    public decimal WinLossReal { get; set; }

    public decimal? MarginReal { get; set; }
    
}
