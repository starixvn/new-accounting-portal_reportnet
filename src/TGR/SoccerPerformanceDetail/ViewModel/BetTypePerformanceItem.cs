namespace SoccerPerformanceDetail.ViewModel;

public class BetTypePerformanceItem
{
    public int BetTypeId { get; set; }

    public string BetTypeName { get; set; }

    public int NoOfTraders { get; set; }

    public int NoOfLeagues { get; set; }

    public int NoOfMatches { get; set; }

    public decimal Turnover { get; set; }

    public decimal WinLoss { get; set; }

    public decimal? Margin => Turnover != 0 ? WinLoss / Turnover : null;

    public decimal TurnoverReal { get; set; }

    public decimal WinLossReal { get; set; }

    public decimal? MarginReal => TurnoverReal != 0 ? WinLossReal / TurnoverReal : null;

    public int BetCount { get; set; }

    public bool IsLive { get; set; }

    public int FlagForViewReport { get; set; }

    public static BetTypePerformanceItem operator +(BetTypePerformanceItem a, BetTypePerformanceItem b)
    {
        return new BetTypePerformanceItem
        {
            NoOfTraders = a.NoOfTraders + b.NoOfTraders,
            NoOfMatches = a.NoOfMatches + b.NoOfMatches,
            Turnover = a.Turnover + b.Turnover,
            WinLoss = a.WinLoss + b.WinLoss,
            TurnoverReal = a.TurnoverReal + b.TurnoverReal,
            WinLossReal = a.WinLossReal + b.WinLossReal,
            BetCount = a.BetCount + b.BetCount
        };
}