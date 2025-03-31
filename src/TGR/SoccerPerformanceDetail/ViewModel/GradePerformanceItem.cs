namespace SoccerPerformanceDetail.ViewModel;
public class GradePerformanceItem
{
    public int Grade { get; set; }

    public int NoOfTraders { get; set; }

    public int NoOfLeagues { get; set; }

    public int NoOfMatches { get; set; }

    public decimal Turnover { get; set; }

    public decimal WinLoss { get; set; }

    public decimal? Margin => Turnover != 0 ? WinLoss / Turnover * 100 : null;

    public decimal TurnoverReal { get; set; }

    public decimal WinLossReal { get; set; }

    public decimal? MarginReal => TurnoverReal != 0 ? WinLossReal / TurnoverReal * 100 : null;

    public int BetCount { get; set; }

    public decimal? Score { get; set; }

    public bool IsLive { get; set; }

    public static GradePerformanceItem operator +(GradePerformanceItem a, GradePerformanceItem b)
    {
        return new GradePerformanceItem
        {
            NoOfTraders = a.NoOfTraders + b.NoOfTraders,
            NoOfMatches = a.NoOfMatches + b.NoOfMatches,
            NoOfLeagues = a.NoOfLeagues + b.NoOfLeagues,
            Turnover = a.Turnover + b.Turnover,
            WinLoss = a.WinLoss + b.WinLoss,
            TurnoverReal = a.TurnoverReal + b.TurnoverReal,
            WinLossReal = a.WinLossReal + b.WinLossReal,
            BetCount = a.BetCount + b.BetCount
        };
    }
}