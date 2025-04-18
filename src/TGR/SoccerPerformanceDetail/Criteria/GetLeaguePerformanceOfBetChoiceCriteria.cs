namespace SoccerPerformanceDetail.Criteria;

using Fanex.Data;
using Fanex.Data.Repository;

public class GetLeaguePerformanceOfBetChoiceCriteria : CriteriaBase
{
    [SpParam(Ignored = true)]
    public bool IsLive { get; set; }

    public short Market => IsLive ? (short)1 : (short)0;
    public short BetType { get; set; }

    public string BetChoice { get; set; }

    public string BetTeam { get; set; }

    public decimal Oddsspread { get; set; }

    public bool IsFMBetType { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public bool IsExcludeLeagues { get; set; }

    public string LeagueGroupIds { get; set; }

    public string LeagueName { get; set; }

    [SpParam(Ignored = true)]
    public bool IsReal { get; set; }

    [SpParam(Ignored = true)]
    public bool IsVirtual { get; set; }

    public override string GetSettingKey()
    {
        return "TGRS_Rpt_PerformanceDetail_Bettype_BetChoice_League";
    }

    public override bool IsValid()
    {
        return true;
    }
}
