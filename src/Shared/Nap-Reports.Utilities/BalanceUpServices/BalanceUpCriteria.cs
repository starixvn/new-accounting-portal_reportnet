using Fanex.Data.Repository;

namespace Nap_Reports.Utilities.BalanceUpServices
{
    public class BalanceUpCriteria : CriteriaBase
    {
        public override string GetSettingKey() => "GetBalanceUpRange";

        public override bool IsValid() => true;
    }
}
