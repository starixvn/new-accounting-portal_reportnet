namespace TGR.Model.JuiceLeftOver;

public class LeaguePerformanceInfo : PerformanceInfoBase
{
    public int LeagueId { get; set; }

    public string LeagueName { get; set; } = string.Empty;

    public static LeaguePerformanceInfo operator +(LeaguePerformanceInfo a, LeaguePerformanceInfo b)
    {
        return new LeaguePerformanceInfo
        {
            TraderCount = a.TraderCount + b.TraderCount,
            MatchCount = a.MatchCount + b.MatchCount,
            TurnoverJuice = a.TurnoverJuice + b.TurnoverJuice,
            Juice = a.Juice + b.Juice,
            Turnover = a.Turnover + b.Turnover,
            WinLoss = a.WinLoss + b.WinLoss,
            BetCount = a.BetCount + b.BetCount
        };
    }
}