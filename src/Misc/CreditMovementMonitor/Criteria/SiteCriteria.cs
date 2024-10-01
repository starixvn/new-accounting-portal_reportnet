using Fanex.Data.Repository;

namespace CreditMovementMonitor.Criteria
{
    public class SiteCriteria : CriteriaBase
    {
        public override string GetSettingKey() => "Misc_CreditMovement_GetSites";

        public override bool IsValid() => true;
    }
}
