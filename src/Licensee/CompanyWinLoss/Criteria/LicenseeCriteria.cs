namespace CompanyWinLoss.Criteria;

using Fanex.Data.Repository;

public class LicenseeCriteria : CriteriaBase
{
    public override string GetSettingKey() => "Licensee_CompanyWinloss_GetLicensee";

    public override bool IsValid() => true;
}