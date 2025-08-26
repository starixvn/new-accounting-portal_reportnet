namespace TGR.Model.JuiceLeftOver;

public interface IPerformanceInfo
{
    int TraderCount { get; }

    int MatchCount { get; }

    decimal Turnover { get; }

    decimal TurnoverJuice { get; }

    decimal TurnoverLeftover { get; }

    decimal WinLoss { get; }

    int BetCount { get; }

    decimal Juice { get; }

    decimal WinLossLeftover { get; }

    decimal? JuiceMargin { get; }

    decimal? JuiceMarginTotal { get; }

    decimal? LeftoverMargin { get; }

    decimal? LeftoverMarginTotal { get; }

    decimal? Margin { get; }
} 