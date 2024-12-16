namespace PunterAnalysis.BetCount.Model;

public class BetCountModel
{
    public string Date { get; set; }

    public int BetCount { get; set; }

    public int WinCount { get; set; }

    public int DrawCount { get; set; }

    public decimal WinLoss { get; set; }

    public static BetCountModel operator +(BetCountModel a, BetCountModel b)
    {
        return new BetCountModel
        {
            Date = string.Empty,
            BetCount = a.BetCount + b.BetCount,
            WinCount = a.WinCount + b.WinCount,
            DrawCount = a.DrawCount + b.DrawCount,
            WinLoss = a.WinLoss + b.WinLoss
        };
    }
}