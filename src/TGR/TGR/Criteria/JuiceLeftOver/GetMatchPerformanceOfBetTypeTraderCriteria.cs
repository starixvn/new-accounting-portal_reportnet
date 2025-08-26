namespace TGR.Criteria.JuiceLeftOver;

using Fanex.Data;
using Fanex.Data.Repository;

public class GetMatchPerformanceOfBetTypeTraderCriteria : CriteriaBase
{
    public short BetType { get; set; }

    public int CustId { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public bool IsExcludeLeagues { get; set; }

    [SpParam(Ignored = true)]
    public bool IsViewScore { get; set; }

    public override string GetSettingKey() => "TGRS_Rpt_NewPerformanceDetail_Bettype_Trader_Match";

    public override bool IsValid() => true;
}