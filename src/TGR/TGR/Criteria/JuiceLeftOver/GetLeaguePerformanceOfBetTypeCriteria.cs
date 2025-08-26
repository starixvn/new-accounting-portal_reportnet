namespace TGR.Criteria.JuiceLeftOver;

using Fanex.Data.Repository;

public class GetLeaguePerformanceOfBetTypeCriteria : CriteriaBase
{
    public short BetType { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public bool IsExcludeLeagues { get; set; }

    public override string GetSettingKey() => "TGRS_Rpt_NewPerformanceDetail_Bettype_League";

    public override bool IsValid() => true;
}