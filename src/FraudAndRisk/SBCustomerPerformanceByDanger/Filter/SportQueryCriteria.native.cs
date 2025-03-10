namespace SBCustomerPerformanceByDanger.Filter;

using Fanex.Data.Repository;

public class SportQueryCriteria : CriteriaBase
{
    public override string GetSettingKey() => "Acc_Rpt_SBCustPerfByDangerLevel_SportSel";

    public override bool IsValid() => true;
}