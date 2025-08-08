using Epplus.Mapper.Annotations;

namespace TGR.ViewModel.JuiceLeftOver;

public class BaseExcelItem
{
    [Cell("A5")]
    public int No { get; set; }

    [Cell("H5")]
    public decimal? JuiceMargin { get; set; }

    [Cell("I5")]
    public decimal? JuiceMarginTotal { get; set; }

    [Cell("L5")]
    public decimal? LeftoverMargin { get; set; }

    [Cell("M5")]
    public decimal? LeftoverMarginTotal { get; set; }

    [Cell("P5")]
    public decimal? Margin { get; set; }
}