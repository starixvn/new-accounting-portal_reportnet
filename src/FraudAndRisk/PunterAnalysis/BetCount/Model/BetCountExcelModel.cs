namespace PunterAnalysis.BetCount.Model;

using Epplus.Mapper.Annotations;

public class BetCountExcelModel
{
    [Cell("A10")]
    public string Date { get; set; }

    [Cell("B10")]
    public int BetCount { get; set; }

    [Cell("C10")]
    public int WinCount { get; set; }

    [Cell("D10")]
    public int DrawCount { get; set; }

    [Cell("E10")]
    public decimal WinLoss { get; set; }
}