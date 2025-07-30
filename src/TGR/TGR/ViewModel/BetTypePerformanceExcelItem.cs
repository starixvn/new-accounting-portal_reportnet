using Epplus.Mapper.Annotations;

namespace TGR.ViewModel;

public class BetTypePerformanceExcelItem : BasePerformanceExcelItem
{

    [Cell("B5")]
    public string BetTypeName { get; set; }


    [Cell("D5")]
    public int NoOfTraders { get; set; }

    [Cell("E5")]
    public int NoOfLeagues { get; set; }

    [Cell("G5")]
    public int BetCount { get; set; }

    [Cell("I5")]
    public decimal? AvgToPerMatch { get; set; }

    [Cell("J5")]
    public decimal WinLoss { get; set; }


    [Cell("M5")]
    public decimal? AvgToPerMatchReal { get; set; }

    [Cell("N5")]
    public decimal WinLossReal { get; set; }
}