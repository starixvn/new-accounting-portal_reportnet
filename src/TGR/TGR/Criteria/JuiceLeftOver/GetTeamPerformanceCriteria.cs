namespace TGR.Criteria.JuiceLeftOver;

using Fanex.Data;
using Fanex.Data.Repository;

public class GetTeamPerformanceCriteria : CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string TeamIds { get; set; }

    public bool IsExcludeLeagues { get; set; }

    [SpParam(Ignored = true)]
    public bool IsViewScore { get; set; }

    public override string GetSettingKey() => "TGRS_Rpt_NewPerformanceDetail_Team";

    public override bool IsValid() => true;
}