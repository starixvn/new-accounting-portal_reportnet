using Fanex.Data.Repository;

namespace CompanyBalanceDifference.Criteria
{
    public class CompanyBalanceDifferenceCriteria : CriteriaBase
    {
        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "Balance_CompanyBalanceDifference_GetReport";
        }
    }
}
