namespace TGR.ViewModel.SoccerPerformanceDetail;

public class MatchPerformanceItem
{
    public int LeagueId { get; set; }

    public int HomeId { get; set; }

    public int AwayId { get; set; }

    public int MatchId { get; set; }

    public string MatchName { get; set; }

    public string EventDate { get; set; }

    public int BetCount { get; set; }

    public decimal Turnover { get; set; }

    public decimal WinLoss { get; set; }

    public decimal? Margin => Turnover != 0 ? WinLoss / Turnover * 100 : null;

    public decimal TurnoverReal { get; set; }

    public decimal WinLossReal { get; set; }

    public decimal? MarginReal => TurnoverReal != 0 ? WinLossReal / TurnoverReal * 100 : null;

    public decimal? Score { get; set; }

    public decimal WinLossScore { get; set; }

    public decimal SumCategory { get; set; }

    public bool IsLive { get; set; }

    public static MatchPerformanceItem operator +(MatchPerformanceItem a, MatchPerformanceItem b)
    {
        return new MatchPerformanceItem
        {
            Turnover = a.Turnover + b.Turnover,
            WinLoss = a.WinLoss + b.WinLoss,
            TurnoverReal = a.TurnoverReal + b.TurnoverReal,
            WinLossReal = a.WinLossReal + b.WinLossReal,
            BetCount = a.BetCount + b.BetCount
        };
    }
}