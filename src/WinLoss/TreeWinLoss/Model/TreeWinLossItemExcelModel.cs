using Epplus.Mapper.Annotations;

namespace TreeWinLoss.Model;

public class TreeWinLossItemByProductExcelModel
{
    [Cell("A6")]
    public string Name { get; set; }

    [Cell("B6")]
    public decimal Turnover { get; set; }

    [Cell("C6")]
    public decimal ExtraAmount { get; set; }

    [Cell("D6")]
    public decimal WinLoss { get; set; }

    [Cell("E6")]
    public decimal Commission { get; set; }

    [Cell("F6")]
    public decimal WinLossTotal { get; set; }

    [Cell("G6")]
    public decimal BaseRMTurnover { get; set; }

    [Cell("H6")]
    public decimal BaseRMVirtualTurnover { get; set; }

    [Cell("I6")]
    public decimal BaseRMTurnoverVariancePlus { get; set; }

    [Cell("J6")]
    public decimal BaseRMTurnoverVarianceMinus { get; set; }

    [Cell("K6")]
    public decimal BaseRMTurnoverVarianceAbsoluteTotal { get; set; }

    [Cell("L6")]
    public decimal BaseRMWinLossVariancePlus { get; set; }

    [Cell("M6")]
    public decimal BaseRMWinLossVarianceMinus { get; set; }

    [Cell("N6")]
    public decimal BaseRMWinLossVarianceTotal { get; set; }

    [Cell("O6")]
    public string BaseRMVarianceMargin { get; set; }

    [Cell("P6")]
    public decimal BaseRMTotal { get; set; }

    [Cell("Q6")]
    public decimal BaseRMVirtualTotal { get; set; }

    [Cell("R6")]
    public string BaseRMMargin { get; set; }

    [Cell("S6")]
    public string VirtualBaseRMMargin { get; set; }
}