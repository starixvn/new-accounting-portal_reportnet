using Epplus.Mapper.Annotations;

namespace PunterAnalysis.SportType.Model;

public class SportTypeExcelModel
{
    [Cell("A8")]
    public string SportName { get; set; }

    [Cell("B8")]
    public decimal WinLoss { get; set; }

    [Cell("C8")]
    public decimal Turnover { get; set; }

    [Cell("D8")]
    public object Margin => Turnover == 0
        ? "N/A" :
        WinLoss / Turnover;

    [Cell("E8")]
    public int BetCount { get; set; }
}