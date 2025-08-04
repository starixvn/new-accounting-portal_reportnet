using Fanex.Data;
using Fanex.Data.Repository;

namespace TGR.Criteria;

public class GetMatchPerformanceOfLeagueTraderCriteria : CriteriaBase
{
    [SpParam(Ignored = true)]
    public bool IsLive { get; set; }

    public short Market => IsLive ? (short)1 : (short)0;

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int LeagueId { get; set; }

    public int CustId { get; set; }

    public string BetTypes { get; set; }

    public bool IsExcludeLeagues { get; set; }

    public string MatchGroup { get; set; }

    public string VrSettings { get; set; }

    public string Sites { get; set; }
    public override string GetSettingKey()
    {
        return "TGRS_Rpt_PerformanceDetail_League_Trader_Match";
    }

    public override bool IsValid()
    {
        return true;
    }
}