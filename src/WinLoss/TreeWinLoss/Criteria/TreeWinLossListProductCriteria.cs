namespace TreeWinLoss.Criteria;

using Fanex.Data.Repository;

public class TreeWinLossListProductCriteria : CriteriaBase
{
    public override bool IsValid() => true;

    public override string GetSettingKey() => "Acc_Rpt_TreeWinlostListProduct";
}