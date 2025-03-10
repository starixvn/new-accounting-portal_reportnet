namespace SoccerPerformanceDetail.Criteria;

using Fanex.Data;
using Fanex.Data.Repository;

public class GetBettypeDetailGradeCriteria : CriteriaBase
{
    [SpParam(Ignored = true)]
    public bool IsLive { get; set; }

    public Int16 Market
    {
        get
        {
            if (IsLive)
            {
                return 1;
            }

            return 0;
        }
    }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int CustId { get; set; }

    public int MatchId { get; set; }

    public string Bettypes { get; set; }

    public bool IsExcludeLeagues { get; set; }

    public string LeagueGroupIds { get; set; }

    public override string GetSettingKey()
    {
        return "TGRS_Rpt_PerformanceDetail_Grade_Trader_Match_Bettype";
    }

    public override bool IsValid()
    {
        return true;
    }
}