using Fanex.Data.Repository;

namespace TGR.Criteria.SoccerPerformanceDetail;

public class GetTeamsCriteria : CriteriaBase
{
    public override string GetSettingKey()
    {
        return "TGRS_Team_SelList";
    }

    public override bool IsValid()
    {
        return true;
    }
}