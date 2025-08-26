using Fanex.Data;
using Fanex.Data.Repository;

namespace TGR.Criteria.SoccerPerformanceDetail;

public class GetIntervalPerformanceOfBetTypeLeagueBetChoiceCriteria : CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    [SpParam(Ignored = true)]
    public bool IsLive { get; set; }

    public short Market => IsLive ? (short)1 : (short)0;

    public int LeagueId { get; set; }

    public short BetType { get; set; }

    public string BetTeam { get; set; }

    public decimal Hdp2 { get; set; }

    public decimal Oddsspread { get; set; }

    public bool IsExcludeLeagues { get; set; }

    public string MatchGroup { get; set; }

    public string VrSettings { get; set; }

    public string Sites { get; set; }

    public override string GetSettingKey()
        => "TGRS_Rpt_PerformanceDetail_Bettype_League_BetChoice_Interval";

    public override bool IsValid()
        => true;
}
