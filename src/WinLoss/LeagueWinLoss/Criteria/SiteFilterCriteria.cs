using Fanex.Data.Repository;

namespace LeagueWinLoss.Criteria
{
    public class SiteFilterCriteria : CriteriaBase
    {
        public override string GetSettingKey()
        {
            return "WinLoss_LeagueWinLoss_GetSiteFilter";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}