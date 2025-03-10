namespace SoccerPerformanceDetail.Criteria;

using Fanex.Data;
using Fanex.Data.Repository;

public class GetLeagueOfBetTypeBetChoicePointHDPCriteria : CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string LeagueName { get; set; }

    public string LeagueGroupIds { get; set; }

    public short BetType { get; set; }

    public string BetTeam { get; set; }

    public decimal PointHDP { get; set; }

    public bool IsExcludeLeagues { get; set; }

    public short Market => IsLive ? (short)1 : (short)0;

    [SpParam(Ignored = true)]
    public bool IsLive { get; set; }

    public override string GetSettingKey()
        => "TGRS_Rpt_PerformanceDetail_Bettype_BetChoice_PointHDP_League";

    public override bool IsValid()
        => true;
}
