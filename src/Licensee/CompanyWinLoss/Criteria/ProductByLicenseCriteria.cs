using Fanex.Data.Repository;

namespace CompanyWinLoss.Criteria;

public class ProductByLicenseCriteria : CriteriaBase
{
    public int LicenseeId { get; set; }

    public override bool IsValid() => true;

    public override string GetSettingKey() => "Acc_GetProductListByLicenseId";
}