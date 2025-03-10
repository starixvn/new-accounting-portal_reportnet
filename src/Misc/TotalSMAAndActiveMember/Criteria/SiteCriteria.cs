namespace TotalSMAAndActiveMember.Criteria;

using Fanex.Data.Repository;

public class SiteCriteria : CriteriaBase
{
    public override string GetSettingKey()
    {
        return "Misc_TotalSMAMemberActive_GetSites";
    }

    public override bool IsValid()
    {
        return true;
    }
}