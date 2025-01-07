namespace PunterAnalysis.League.Model;

public class PeriodGroup
{
    public decimal WinLoss { get; set; }

    public decimal Turnover { get; set; }

    public decimal Margin => Turnover == 0 ? 0 : WinLoss / Turnover;

    public int BetCount { get; set; }

    public static PeriodGroup operator +(PeriodGroup a, PeriodGroup b)
    {
        return new PeriodGroup
        {
            BetCount = a.BetCount + b.BetCount,
            WinLoss = a.WinLoss + b.WinLoss,
            Turnover = a.Turnover + b.Turnover
        };
    }
}