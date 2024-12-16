using Epplus.Mapper.Annotations;

namespace PunterAnalysis.GameBetType.Model;

public class GameBetTypeExcelModel
{
    [Cell("A12")]
    public string BetTypeName { get; set; }

    [Cell("B12")]
    public decimal NonLiveWinLoss { get; set; }

    [Cell("C12")]
    public decimal NonLiveTurnover { get; set; }

    [Cell("D12")]
    public object NonLiveMargin => NonLiveTurnover == 0
        ? "N/A" :
        NonLiveWinLoss / NonLiveTurnover;

    [Cell("E12")]
    public int NonLiveBetCount { get; set; }

    [Cell("F12")]
    public decimal LiveWinLoss { get; set; }

    [Cell("G12")]
    public decimal LiveTurnover { get; set; }

    [Cell("H12")]
    public object LiveMargin => LiveTurnover == 0
        ? "N/A" :
        LiveWinLoss / LiveTurnover;

    [Cell("I12")]
    public int LiveBetCount { get; set; }

    [Cell("J12")]
    public decimal WinLoss { get; set; }

    [Cell("K12")]
    public decimal Turnover { get; set; }

    [Cell("L12")]
    public object Margin => Turnover == 0
        ? "N/A" :
        WinLoss / Turnover;

    [Cell("M12")]
    public int BetCount { get; set; }
}