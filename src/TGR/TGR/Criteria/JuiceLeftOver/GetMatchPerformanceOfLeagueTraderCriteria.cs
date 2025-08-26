namespace TGR.Criteria.JuiceLeftOver;

using Fanex.Data.Repository;

public class GetMatchPerformanceOfLeagueTraderCriteria : CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int LeagueId { get; set; }

    public int CustId { get; set; }

    public bool IsExcludeLeagues { get; set; }

    public override string GetSettingKey() => "TGRS_Rpt_NewPerformanceDetail_League_Trader_Match";

    public override bool IsValid() => true;
}