using Fanex.Data.Repository;

namespace CashOutPerformance.Criteria
{
    public class LicenseeCriteria : CriteriaBase
    {
        public override string GetSettingKey() => "Acc_Rpt_COPerformance_LicenseesSel";

        public override bool IsValid() => true;
    }
}
