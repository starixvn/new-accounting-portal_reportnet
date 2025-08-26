using Fanex.Data;
using Fanex.Data.Repository;

namespace TGR.Criteria.SoccerPerformanceDetail;

public class GetTraderPerformanceOfBetTypeLeagueCriteria : CriteriaBase
{
    [SpParam(Ignored = true)]
    public bool IsLive { get; set; }

    public short Market => IsLive ? (short)1 : (short)0;

    public short BetType { get; set; }

    public int LeagueId { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public bool IsExcludeLeagues { get; set; }

    public string MatchGroup { get; set; }

    public string VrSettings { get; set; }

    public string Sites { get; set; }

    public override string GetSettingKey()
    {
        return "TGRS_Rpt_PerformanceDetail_Bettype_League_Trader";
    }

    public override bool IsValid()
    {
        return true;
    }
}