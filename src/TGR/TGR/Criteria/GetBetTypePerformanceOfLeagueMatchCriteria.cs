using Fanex.Data;
using Fanex.Data.Repository;

namespace TGR.Criteria;

public class GetBetTypePerformanceOfLeagueMatchCriteria : CriteriaBase
{
    [SpParam(Ignored = true)]
    public bool IsLive { get; set; }

    public short Market => IsLive ? (short)1 : (short)0;

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int LeagueId { get; set; }

    public int MatchId { get; set; }

    public string Bettypes { get; set; }

    public bool IsExcludeLeagues { get; set; }

    public string MatchGroup { get; set; }

    public string VrSettings { get; set; }

    public override string GetSettingKey()
    {
        return "TGRS_Rpt_PerformanceDetail_League_Match_Bettype";
    }

    public override bool IsValid()
    {
        return true;
    }
}