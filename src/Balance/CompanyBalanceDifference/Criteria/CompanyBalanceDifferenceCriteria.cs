using Fanex.Data.Repository;

namespace CompanyBalanceDifference.Criteria
{
    public class CompanyBalanceDifferenceCriteria : CriteriaBase
    {
        public override bool IsValid() => true;

        public override string GetSettingKey() => "Balance_CompanyBalanceDifference_GetReport";
    }
}
