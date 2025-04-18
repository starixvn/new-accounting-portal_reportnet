namespace CreditMovementMonitor.Criteria;

using Fanex.Data.Repository;

public class SiteCriteria : CriteriaBase
{
    public override string GetSettingKey() => "Misc_CreditMovement_GetSites";

    public override bool IsValid() => true;
}