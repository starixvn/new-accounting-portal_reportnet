
using Fanex.Data.Repository;

namespace TGR.Criteria.SoccerPerformanceDetail;

public class GetLeagueGroupSoccerCriteria : CriteriaBase
{
    public string LeagueGroupIds { get; set; }

    public override string GetSettingKey()
    {
        return "TGRS_LeagueGroupSoccer_Get";
    }

    public override bool IsValid()
    {
        return true;
    }

}
