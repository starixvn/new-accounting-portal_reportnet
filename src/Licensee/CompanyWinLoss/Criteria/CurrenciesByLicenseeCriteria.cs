using Fanex.Data.Repository;

namespace CompanyWinLoss.Criteria
{
    public class CurrenciesByLicenseeCriteria : CriteriaBase
    {
        public int LicenseeID { get; set; }

        public bool ExcludeTest { get; set; }

        public override string GetSettingKey() => "Licensee_Core_GetCurrencies";

        public override bool IsValid() => true;
    }
}
