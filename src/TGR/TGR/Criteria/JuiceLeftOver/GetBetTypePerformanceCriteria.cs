namespace TGR.Criteria.JuiceLeftOver;

using Fanex.Data.Repository;

public class GetBetTypePerformanceCriteria : CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public bool IsExcludeLeagues { get; set; }

    public override string GetSettingKey() => "TGRS_Rpt_NewPerformanceDetail_BetType";

    public override bool IsValid() => true;
}