using Fanex.Data.Repository;

namespace PeakConcurrentUsers.Criteria;

public class LicenseeCriteria : CriteriaBase
{
    public override string GetSettingKey() => "Licensee_PeakConcurrentUser_GetLicensee";

    public override bool IsValid() => true;
}