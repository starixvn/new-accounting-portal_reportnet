namespace TGR.Model.JuiceLeftOver;

public class BetTypePerformanceInfo : PerformanceInfoBase
{
    public short BetType { get; set; }

    public string TypeName { get; set; } = string.Empty;

    public int LeagueCount { get; set; }

    public decimal? Score { get; set; }
}