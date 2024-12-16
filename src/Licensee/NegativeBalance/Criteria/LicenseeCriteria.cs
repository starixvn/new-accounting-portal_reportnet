using Fanex.Data.Repository;

namespace NegativeBalance.Criteria;

public class LicenseeCriteria : CriteriaBase, ICriteria
{
    public override string GetSettingKey() => "Licensee_NegativeBalance_GetLicensee";

    public override bool IsValid() => true;
}