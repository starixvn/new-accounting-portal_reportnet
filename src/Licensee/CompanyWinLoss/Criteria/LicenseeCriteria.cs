using Fanex.Data.Repository;

namespace CompanyWinLoss.Criteria
{
    public class LicenseeCriteria : CriteriaBase
    {
        public override string GetSettingKey() => "Licensee_CompanyWinloss_GetLicensee";

        public override bool IsValid() => true;
    }
}
