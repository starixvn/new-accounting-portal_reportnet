namespace TGR.Criteria.JuiceLeftOver;

using Fanex.Data.Repository;

public class GetLeaguePerformanceCriteria : CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string LeagueName { get; set; }

    public bool IsExcludeLeagues { get; set; }

    public override string GetSettingKey() => "TGRS_Rpt_NewPerformanceDetail_League";

    public override bool IsValid() => true;
}