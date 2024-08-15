using Fanex.Data.Repository;

namespace CompanyWinLoss.Criteria
{
    public class ProductByLicenseCriteria : CriteriaBase
    {
        public int LicenseeId { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "Acc_GetProductListByLicenseId";
        }
    }
}
