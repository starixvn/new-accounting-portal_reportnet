namespace TGR.Criteria.JuiceLeftOver;

using Fanex.Data;
using Fanex.Data.Repository;

public class GetBetTypePerformanceOfLeagueMatchCriteria : CriteriaBase
{
    public DateTime FromDate { get; set; } = default;

    public DateTime ToDate { get; set; } = default;

    public int LeagueId { get; set; } = default;

    public int MatchId { get; set; } = default;

    public bool IsExcludeLeagues { get; set; } = default;

    [SpParam(Ignored = true)]
    public bool IsViewScore { get; set; } = default;

    public override string GetSettingKey() => "TGRS_Rpt_NewPerformanceDetail_League_Match_Bettype";

    public override bool IsValid() => true;
}