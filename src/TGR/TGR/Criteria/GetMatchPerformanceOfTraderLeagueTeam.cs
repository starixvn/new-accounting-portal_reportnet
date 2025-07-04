using Fanex.Data;
using Fanex.Data.Repository;

namespace TGR.Criteria;

public class GetMatchPerformanceOfTraderLeagueTeam : CriteriaBase
{
    [SpParam(Ignored = true)]
    public bool IsLive { get; set; }

    public short Market => IsLive ? (short)1 : (short)0;

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public short TeamId { get; set; }

    public int LeagueId { get; set; }

    public int CustId { get; set; }

    public bool IsExcludeLeagues { get; set; }

    public string Bettypes { get; set; }

    public string MatchGroup { get; set; }

    public string VrSettings { get; set; }

    public override bool IsValid()
    {
        return true;
    }

    public override string GetSettingKey()
    {
        return "TGRS_Rpt_PerformanceDetail_Team_League_Trader_Match";
    }
}