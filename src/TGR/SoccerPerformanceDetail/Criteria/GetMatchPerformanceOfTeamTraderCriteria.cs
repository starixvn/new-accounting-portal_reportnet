namespace SoccerPerformanceDetail.Criteria;

using Fanex.Data;
using Fanex.Data.Repository;

public class GetMatchPerformanceOfTeamTraderCriteria : CriteriaBase
{
    [SpParam(Ignored = true)]
    public bool IsLive { get; set; }

    public short Market => IsLive ? (short)1 : (short)0;
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public short TeamId { get; set; }

    public int CustId { get; set; }

    public string BetTypes { get; set; }

    public bool IsExcludeLeagues { get; set; }

    public string LeagueGroupIds { get; set; }

    public override string GetSettingKey()
    {
        return "TGRS_Rpt_PerformanceDetail_Team_Trader_Match";
    }

    public override bool IsValid()
    {
        return true;
    }
}