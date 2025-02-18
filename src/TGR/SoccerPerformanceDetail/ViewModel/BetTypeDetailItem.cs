namespace SoccerPerformanceDetail.ViewModel;


public class BetTypeDetailItem
{
    public short BetType { get; set; }

    public string TypeName { get; set; }

    public decimal Turnover { get; set; }

    public decimal WinLoss { get; set; }     

    public decimal? Margin { get; set; }

    public decimal TurnoverReal { get; set; }

    public decimal WinLossReal { get; set; }

    public decimal? MarginReal { get; set; }

    public int BetCount { get; set; }

    public decimal? Benchmark { get; set; }

    public decimal? Score { get; set; }

    public List<ScoreByPeriodItem> ScoreByPeriod { get; set; }
}