namespace TGR.Model.JuiceLeftOver;

public class MatchPerformanceInfo
{
    public int? LeagueId { get; set; }

    public int? MatchId { get; set; }

    public int? HomeId { get; set; }

    public int? AwayId { get; set; }

    public DateTime EventDate { get; set; }

    public decimal Turnover { get; set; }

    public decimal TurnoverJuice { get; set; }

    public decimal TurnoverLeftover => Turnover - TurnoverJuice;

    public decimal WinLoss { get; set; }

    public int? BetCount { get; set; }

    public decimal? Score { get; set; }

    public decimal Juice { get; set; }

    public decimal WinlostLeftover => WinLoss - Juice;

    public decimal? JuiceMargin => TurnoverJuice != 0 ? ((decimal?)(Juice / TurnoverJuice * 100)) : null;

    public decimal? JuiceMarginTotal => Turnover != 0 ? ((decimal?)(Juice / Turnover * 100)) : null;

    public decimal? LeftoverMargin => TurnoverLeftover != 0 ? ((decimal?)(WinlostLeftover / TurnoverLeftover * 100)) : null;

    public decimal? LeftoverMarginTotal => Turnover != 0 ? ((decimal?)(WinlostLeftover / Turnover * 100)) : null;

    public decimal? Margin => Turnover != 0 ? ((decimal?)(WinLoss / Turnover * 100)) : null;

    public string EventDateString => Convert.ToDateTime(EventDate).ToShortFormatedDate();

    public string LeagueName => CacheRef.GetLeagueName(LeagueId);

    public string MatchName => string.Format("{0} -vs- {1}", CacheRef.GetTeamName(HomeId), CacheRef.GetTeamName(AwayId));
}