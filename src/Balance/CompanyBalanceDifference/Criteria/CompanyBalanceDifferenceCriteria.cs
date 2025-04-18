namespace CompanyBalanceDifference.Criteria;

using Fanex.Data.Repository;

public class CompanyBalanceDifferenceCriteria : CriteriaBase
{
    public override bool IsValid() => true;

    public override string GetSettingKey() => "Balance_CompanyBalanceDifference_GetReport";
}