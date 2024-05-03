using Fanex.Data.Repository;

namespace SBCustomerPerformanceByDanger.Filter
{
    public class SportQueryCriteria : CriteriaBase
    {
        public override string GetSettingKey() => $"Acc_Rpt_SBCustPerfByDangerLevel_SportSel";

        public override bool IsValid() => true;
    }
}