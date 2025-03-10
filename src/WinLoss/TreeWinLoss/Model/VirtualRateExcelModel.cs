namespace TreeWinLoss.Model;

using Epplus.Mapper.Annotations;

public class VirtualRateExcelModel
{
    [Cell("A5")]
    public int No { get; set; }

    [Cell("B5")]
    public string User { get; set; }

    [Cell("C5")]
    public string Sport { get; set; }

    [Cell("D5")]
    public decimal? Hdp { get; set; }

    [Cell("E5")]
    public decimal? Ou { get; set; }

    [Cell("F5")]
    public decimal? Oe { get; set; }

    [Cell("G5")]
    public decimal? Hdp1 { get; set; }

    [Cell("H5")]
    public decimal? Ou1 { get; set; }

    [Cell("I5")]
    public decimal? Oe1 { get; set; }

    [Cell("J5")]
    public decimal? MoneyLine { get; set; }

    [Cell("K5")]
    public decimal? MoneyLine1 { get; set; }

    [Cell("L5")]
    public decimal? SetXGameYWinner { get; set; }
}