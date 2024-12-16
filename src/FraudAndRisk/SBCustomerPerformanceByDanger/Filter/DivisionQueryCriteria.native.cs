using Fanex.Data.Repository;

namespace SBCustomerPerformanceByDanger.Filter;

public class DivisionQueryCriteria : CriteriaBase
{
    public override string GetSettingKey() => "Acc_Rpt_SBCustPerfByDangerLevel_DivisionSel";

    public override bool IsValid() => true;
}