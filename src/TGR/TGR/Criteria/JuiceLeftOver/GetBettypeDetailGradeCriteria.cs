namespace TGR.Criteria.JuiceLeftOver;

using Fanex.Data.Repository;

public class GetBettypeDetailGradeCriteria : CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int CustId { get; set; }

    public int MatchId { get; set; }

    public bool IsExcludeLeagues { get; set; }

    public override string GetSettingKey() => "TGRS_Rpt_NewPerformanceDetail_League_Trader_Match_Bettype";

    public override bool IsValid() => true;
}