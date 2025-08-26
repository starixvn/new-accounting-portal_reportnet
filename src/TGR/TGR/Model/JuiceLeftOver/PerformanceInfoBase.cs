namespace TGR.Model.JuiceLeftOver;

public abstract class PerformanceInfoBase : IPerformanceInfo
{
    public int TraderCount { get; set; }

    public int MatchCount { get; set; }

    public decimal Turnover { get; set; }

    public decimal TurnoverJuice { get; set; }

    public decimal TurnoverLeftover => Turnover - TurnoverJuice;

    public decimal WinLoss { get; set; }

    public int BetCount { get; set; }

    public decimal Juice { get; set; }

    public decimal WinLossLeftover => WinLoss - Juice;

    public decimal? JuiceMargin => TurnoverJuice != 0 ? Juice / TurnoverJuice * 100 : null;

    public decimal? JuiceMarginTotal => Turnover != 0 ? Juice / Turnover * 100 : null;

    public decimal? LeftoverMargin => TurnoverLeftover != 0 ? WinLossLeftover / TurnoverLeftover * 100 : null;

    public decimal? LeftoverMarginTotal => Turnover != 0 ? WinLossLeftover / Turnover * 100 : null;

    public decimal? Margin => Turnover != 0 ? WinLoss / Turnover * 100 : null;
} 