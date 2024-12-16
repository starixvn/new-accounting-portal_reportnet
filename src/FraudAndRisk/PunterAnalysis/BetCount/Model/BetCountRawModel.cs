namespace PunterAnalysis.BetCount.Model;

public class BetCountRawModel
{
    public DateTime Date { get; set; }

    public int BetCount { get; set; }

    public int WinCount { get; set; }

    public int DrawCount { get; set; }

    public decimal WinLoss { get; set; }
}