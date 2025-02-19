namespace SoccerPerformanceDetail.Criteria;

using Fanex.Data;
using Fanex.Data.Repository;

public class GetMatchPerformanceOfLeagueTraderCriteria : CriteriaBase
{
    [SpParam(Ignored = true)]
    public bool IsLive { get; set; }

    public Int16 Market
    {
        get
        {
            if (this.IsLive)
            {
                return 1;
            }

            return 0;
        }
    }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int LeagueId { get; set; }

    public int CustId { get; set; }

    public string BetTypes { get; set; }

    public bool IsExcludeLeagues { get; set; }

    public override string GetSettingKey()
    {
        return "TGRS_Rpt_PerformanceDetail_League_Trader_Match";
    }

    public override bool IsValid()
    {
        return true;
    }
}