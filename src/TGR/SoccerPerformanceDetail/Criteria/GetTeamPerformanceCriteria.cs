namespace SoccerPerformanceDetail.Criteria;

using Fanex.Data;
using Fanex.Data.Repository;

public class GetTeamPerformanceCriteria : CriteriaBase, IBasePerformanceCriteria
{
    [SpParam(Ignored = true)]
    public Market MarketFilter { get; set; }

    public short Market
    {
        get
        {
            return MarketFilter switch
            {
                SoccerPerformanceDetail.Market.Live => 1,
                SoccerPerformanceDetail.Market.NonLive => 0,
                _ => -1
            };
        }
    }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string TeamIds { get; set; }

    public decimal? Turnover { get; set; }

    public decimal? WinLoss { get; set; }

    public decimal? MarginFrom { get; set; }

    public decimal? MarginTo { get; set; }

    [SpParam(Ignored = true)]
    public bool IsReal { get; set; }

    [SpParam(Ignored = true)]
    public bool IsVirtual { get; set; }

    public decimal? TurnoverReal { get; set; }

    public decimal? WinLossReal { get; set; }

    public decimal? MarginFromReal { get; set; }

    public decimal? MarginToReal { get; set; }

    public string BetTypes { get; set; }

    public bool IsExcludeLeagues { get; set; }

    public string LeagueGroupIds { get; set; }

    [SpParam(Ignored = true)]
    public bool IsAllLeagueGroup { get; set; }

    public override string GetSettingKey()
    {
        return "TGRS_Rpt_PerformanceDetail_Team";
    }

    public override bool IsValid()
    {
        return true;
    }
}