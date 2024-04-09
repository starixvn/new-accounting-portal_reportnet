using Fanex.Data.Repository;

namespace AbnormalBet.Criteria
{
    public class ReasonQueryCriteria : CriteriaBase
    {
        public override bool IsValid() => true;

        public override string GetSettingKey() => $"Acc_Rpt_AbnormalBet_ReasonSel";
    }
}
