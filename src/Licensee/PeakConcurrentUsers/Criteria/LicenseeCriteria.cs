namespace PeakConcurrentUsers.Criteria;

using Fanex.Data.Repository;

public class LicenseeCriteria : CriteriaBase
{
    public override string GetSettingKey() => "Licensee_PeakConcurrentUser_GetLicensee";

    public override bool IsValid() => true;
}