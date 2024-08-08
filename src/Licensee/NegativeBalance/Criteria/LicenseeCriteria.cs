using Fanex.Data.Repository;

namespace NegativeBalance.Criteria
{
    public class LicenseeCriteria : CriteriaBase, ICriteria
    {
        public override string GetSettingKey()
        {
            return "Licensee_NegativeBalance_GetLicensee";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
