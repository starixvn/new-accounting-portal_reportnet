namespace SoccerPerformanceDetail.Criteria;

using Fanex.Data;
using Fanex.Data.Repository;

public class GetBetTypePerformanceCriteria : CriteriaBase, IBasePerformanceCriteria
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

    public string BetTypes { get; set; }

    public string LeagueGroupIds { get; set; }

    public string VrSettings { get; set; }

    public string MatchGroup { get; set; }

    [SpParam(Ignored = true)]
    public bool IsAllLeagueGroup { get; set; }

    public string LeagueName { get; set; }


    public override string GetSettingKey()
    {
        return "TGRS_Rpt_PerformanceDetail_BetType";
    }

    public override bool IsValid()
    {
        return true;
    }
}