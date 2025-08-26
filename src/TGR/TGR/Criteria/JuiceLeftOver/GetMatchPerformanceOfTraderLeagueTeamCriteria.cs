namespace TGR.Criteria.JuiceLeftOver;

using Fanex.Data.Repository;

public class GetMatchPerformanceOfTraderLeagueTeamCriteria : CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public short TeamId { get; set; }

    public int LeagueId { get; set; }

    public int CustId { get; set; }

    public bool IsExcludeLeagues { get; set; }

    public override bool IsValid() => true;

    public override string GetSettingKey() => "TGRS_Rpt_NewPerformanceDetail_Team_League_Trader_Match";
}
