namespace TGR.Criteria.JuiceLeftOver;

using Fanex.Data;
using Fanex.Data.Repository;

public class GetTraderPerformanceOfBetTypeCriteria : CriteriaBase
{
    public short BetType { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public bool IsExcludeLeagues { get; set; }

    [SpParam(Ignored = true)]
    public bool IsViewScore { get; set; }

    public override string GetSettingKey() => "TGRS_Rpt_NewPerformanceDetail_Bettype_Trader";

    public override bool IsValid() => true;
}