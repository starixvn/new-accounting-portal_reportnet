namespace AbnormalBet.Criteria;

using Fanex.Data.Repository;

public class ReasonQueryCriteria : CriteriaBase
{
    public override bool IsValid() => true;

    public override string GetSettingKey() => "Acc_Rpt_AbnormalBet_ReasonSel";
}