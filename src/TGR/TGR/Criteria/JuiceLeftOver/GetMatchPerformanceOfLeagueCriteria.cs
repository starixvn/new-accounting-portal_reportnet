namespace TGR.Criteria.JuiceLeftOver;

using Fanex.Data.Repository;

public class GetMatchPerformanceOfLeagueCriteria : CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int LeagueId { get; set; }

    public bool IsExcludeLeagues { get; set; }

    public override string GetSettingKey() => "TGRS_Rpt_NewPerformanceDetail_League_Match";

    public override bool IsValid() => true;
}