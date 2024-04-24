using Fanex.Data.Repository;

namespace CashOutPerformance.Criteria
{
    public class SportCriteria : CriteriaBase
    {
        public override string GetSettingKey() => "Acc_Rpt_COPerformance_SportSel";

        public override bool IsValid() => true;
    }
}