using Fanex.Data.Repository;

namespace TreeWinLoss.Criteria
{
    public class TreeWinLossListProductCriteria : CriteriaBase
    {
        public override bool IsValid() => true;

        public override string GetSettingKey() => "Acc_Rpt_TreeWinlostListProduct";
    }
}