namespace SoccerPerformanceDetail.Criteria;

using Fanex.Data;
using Fanex.Data.Repository;

public class GetGradePerformanceCriteria : CriteriaBase
{
    [SpParam(Ignored = true)]
    public Market MarketFilter { get; set; }

    public Int16 Market
    {
        get
        {
            switch (this.MarketFilter)
            {
                case SoccerPerformanceDetail.Market.Live:
                    return 1;
                case SoccerPerformanceDetail.Market.NonLive:
                    return 0;
                default:
                    return -1;
            }
        }
    }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

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
        return "TGRS_Rpt_PerformanceDetail_Grade";
    }

    public override bool IsValid()
    {
        return true;
    }
}