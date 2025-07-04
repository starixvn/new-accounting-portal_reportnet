using Fanex.Data.Repository;

namespace TGR.Criteria;

public class GetScoreByPeriodCriteria : CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int CustId { get; set; }

    public int MatchId { get; set; }

    public string Bettypes { get; set; }

    public bool IsExcludeLeagues { get; set; }

    public string LeagueGroupIds { get; set; }

    public override bool IsValid()
    {
        return true;
    }

    public override string GetSettingKey()
    {
        return "TGRS_Rpt_ScoreDetail";
    }
}
