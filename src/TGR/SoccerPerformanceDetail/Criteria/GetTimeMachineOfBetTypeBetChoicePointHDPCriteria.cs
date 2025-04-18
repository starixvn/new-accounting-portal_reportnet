namespace SoccerPerformanceDetail.Criteria;

using Fanex.Data;
using Fanex.Data.Repository;

public class GetTimeMachineOfBetTypeBetChoicePointHDPCriteria : CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    [SpParam(Ignored = true)]
    public bool IsLive { get; set; }

    public short Market => IsLive ? (short)1 : (short)0;

    public short BetType { get; set; }

    public string BetTeam { get; set; }

    public decimal PointHDP { get; set; }

    public string LeagueGroupIds { get; set; }

    public string LeagueName { get; set; }

    public bool IsExcludeLeagues { get; set; }

    public override string GetSettingKey()
        => "TGRS_Rpt_PerformanceDetail_Bettype_BetChoice_PointHDP_Minute";

    public override bool IsValid()
        => true;
}
