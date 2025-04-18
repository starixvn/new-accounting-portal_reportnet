namespace CashOutPerformance.Criteria;

using Fanex.Data.Repository;

public class LicenseeCriteria : CriteriaBase
{
    public override string GetSettingKey() => "Acc_Rpt_COPerformance_LicenseesSel";

    public override bool IsValid() => true;
}