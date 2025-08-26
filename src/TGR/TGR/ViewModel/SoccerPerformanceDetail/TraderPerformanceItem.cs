namespace TGR.ViewModel.SoccerPerformanceDetail;

public class TraderPerformanceItem
{
    public int TraderId { get; set; }

    public string TraderName { get; set; }

    public int Grade { get; set; }

    public bool HasGradeHistory => NoOfGradeChanged > 1;

    public int TeamId { get; set; }

    public string TeamName { get; set; }

    public bool HasTeamHistory => NoOfTeamChanged > 1;

    public int NoOfLeagues { get; set; }

    public int NoOfMatches { get; set; }

    public int BetCount { get; set; }

    public decimal Turnover { get; set; }

    public decimal WinLoss { get; set; }

    public decimal? Margin => Turnover != 0 ? WinLoss / Turnover * 100 : null;

    public decimal TurnoverReal { get; set; }

    public decimal WinLossReal { get; set; }

    public decimal? MarginReal => TurnoverReal != 0 ? WinLossReal / TurnoverReal * 100 : null;

    public decimal? Score { get; set; }

    public bool IsLive { get; set; }

    public int? NoOfGradeChanged { get; set; }

    public int? NoOfTeamChanged { get; set; }

    public static TraderPerformanceItem operator +(TraderPerformanceItem a, TraderPerformanceItem b)
    {
        return new TraderPerformanceItem
        {
            NoOfLeagues = a.NoOfLeagues + b.NoOfLeagues,
            NoOfMatches = a.NoOfMatches + b.NoOfMatches,
            Turnover = a.Turnover + b.Turnover,
            WinLoss = a.WinLoss + b.WinLoss,
            TurnoverReal = a.TurnoverReal + b.TurnoverReal,
            WinLossReal = a.WinLossReal + b.WinLossReal,
            BetCount = a.BetCount + b.BetCount
        };
    }
}