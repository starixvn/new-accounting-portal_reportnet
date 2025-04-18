namespace SoccerPerformanceDetail.Criteria;

using Fanex.Data.Repository;

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