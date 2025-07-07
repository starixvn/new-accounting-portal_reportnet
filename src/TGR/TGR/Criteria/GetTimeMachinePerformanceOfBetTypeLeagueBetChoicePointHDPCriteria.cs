using Fanex.Data;
using Fanex.Data.Repository;

namespace TGR.Criteria;

public class GetTimeMachinePerformanceOfBetTypeLeagueBetChoicePointHDPCriteria : CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int LeagueId { get; set; }        

    public short BetType { get; set; }

    public string BetTeam { get; set; }

    public decimal PointHDP { get; set; }

    public bool IsExcludeLeagues { get; set; }

    public short Market => IsLive ? (short)1 : (short)0;

    [SpParam(Ignored = true)]
    public bool IsLive { get; set; }

    public string MatchGroup { get; set; }

    public string VrSettings { get; set; }

    public string Sites { get; set; }

    public override string GetSettingKey()
        => "TGRS_Rpt_PerformanceDetail_Bettype_League_BetChoice_PointHDP_Minute";

    public override bool IsValid()
        => true;
}
