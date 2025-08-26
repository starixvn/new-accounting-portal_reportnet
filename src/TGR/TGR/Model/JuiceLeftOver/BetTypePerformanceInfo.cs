namespace TGR.Model.JuiceLeftOver;

public class BetTypePerformanceInfo : PerformanceInfoBase
{
    public short BetType { get; set; }

    public string TypeName { get; set; } = string.Empty;

    public int LeagueCount { get; set; }

    public decimal? Score { get; set; }

    public static BetTypePerformanceInfo operator +(BetTypePerformanceInfo a, BetTypePerformanceInfo b)
    {
        return new BetTypePerformanceInfo
        {
            TraderCount = a.TraderCount + b.TraderCount,
            LeagueCount = a.LeagueCount + b.LeagueCount,
            MatchCount = a.MatchCount + b.MatchCount,
            TurnoverJuice = a.TurnoverJuice + b.TurnoverJuice,
            Juice = a.Juice + b.Juice,
            Turnover = a.Turnover + b.Turnover,
            WinLoss = a.WinLoss + b.WinLoss,
            BetCount = a.BetCount + b.BetCount
        };
    }
}