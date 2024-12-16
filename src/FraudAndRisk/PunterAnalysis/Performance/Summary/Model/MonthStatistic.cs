namespace PunterAnalysis.Performance.Summary.Model;

public class MonthStatistic
{
    public DateTime Date { get; set; }

    public decimal WinLoss { get; set; }

    public decimal Turnover { get; set; }

    public int BetCount { get; set; }
}