namespace TGR.Criteria.JuiceLeftOver;

using Fanex.Data;
using Fanex.Data.Repository;

public class GetTraderPerformanceOfLeagueTeam : CriteriaBase
{
    public DateTime FromDate { get; set; } = default;

    public DateTime ToDate { get; set; } = default;

    public short TeamId { get; set; } = default;

    public int LeagueId { get; set; } = default;

    public bool IsExcludeLeagues { get; set; } = default;

    [SpParam(Ignored = true)]
    public bool IsViewScore { get; set; } = default;

    public override string GetSettingKey() => "TGRS_Rpt_NewPerformanceDetail_Team_League_Trader";

    public override bool IsValid() => true;
}
