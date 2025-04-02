namespace SoccerPerformanceDetail.ViewModel;

public class BasePerformanceExcelItem
{
    public int No { get; set; }
    public string Market { get; set; }
    public bool IsLive { get; set; }
    public decimal? Margin { get; set; }
    public decimal? MarginReal { get; set; }
    public int NoOfMatches { get; set; }
    public decimal Turnover { get; set; }
    public decimal TurnoverReal { get; set; }
} 