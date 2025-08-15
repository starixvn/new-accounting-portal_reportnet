using Fanex.Data;
using Fanex.Data.Repository;

namespace TGR.Criteria;

public class GetLeaguePerformanceCriteria : CriteriaBase, IBasePerformanceCriteria
{
    [SpParam(Ignored = true)]
    public Market MarketFilter { get; set; }

    public short Market
    {
        get
        {
            return MarketFilter switch
            {
                TGR.Market.Live => 1,
                TGR.Market.NonLive => 0,
                _ => -1
            };
        }
    }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string LeagueGroupIds { get; set; }
    public string VrSettings { get; set; }
    public string MatchGroup { get; set; }

    public string LeagueName { get; set; }

    public string Sites { get; set; }

    public string BetTypes { get; set; }

    public bool IsExcludeLeagues { get; set; }

    public override string GetSettingKey()
    {
        return "TGRS_Rpt_PerformanceDetail_League";
    }

    public override bool IsValid()
    {
        return true;
    }
}