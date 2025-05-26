namespace SoccerPerformanceDetail.Criteria;

using Fanex.Data;
using Fanex.Data.Repository;

public class GetTraderPerformanceOfGradeCriteria : CriteriaBase
{
    [SpParam(Ignored = true)]
    public bool IsLive { get; set; }

    public short Market => IsLive ? (short)1 : (short)0;

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public byte Grade { get; set; }

    public string BetTypes { get; set; }

    public bool IsExcludeLeagues { get; set; }

    public string LeagueGroupIds { get; set; }
    public string MatchGroup { get; set; }

    public string VrSettings { get; set; }

    public override string GetSettingKey()
    {
        return "TGRS_Rpt_PerformanceDetail_Grade_Trader";
    }

    public override bool IsValid()
    {
        return true;
    }
}