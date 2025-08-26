namespace TGR.Model.JuiceLeftOver;

public class BetTypeTraderPerformanceInfo
{
    public int CustId { get; set; }

    public string TraderName { get; set; }

    public byte? Grade { get; set; }

    public string TeamName { get; set; }

    public int MatchCount { get; set; }

    public int LeagueCount { get; set; }

    public decimal Turnover { get; set; }

    public decimal TurnoverJuice { get; set; }

    public decimal TurnoverLeftover => Turnover - TurnoverJuice;

    public decimal WinLoss { get; set; }

    public int BetCount { get; set; }

    public decimal? Score { get; set; }

    public decimal Juice { get; set; }

    public decimal WinLossLeftover => WinLoss - Juice;

    public decimal? JuiceMargin => TurnoverJuice != 0 ? ((decimal?)(Juice / TurnoverJuice * 100)) : null;

    public decimal? JuiceMarginTotal => Turnover != 0 ? ((decimal?)(Juice / Turnover * 100)) : null;

    public decimal? LeftoverMargin => TurnoverLeftover != 0 ? ((decimal?)(WinLossLeftover / TurnoverLeftover * 100)) : null;

    public decimal? LeftoverMarginTotal => Turnover != 0 ? ((decimal?)(WinLossLeftover / Turnover * 100)) : null;

    public decimal? Margin => Turnover != 0 ? ((decimal?)(WinLoss / Turnover * 100)) : null;
}