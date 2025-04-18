namespace SabaPromotion.Criteria;

using Fanex.Data.Repository;

public class PromotionTypeCriteria: CriteriaBase
{
    public override string GetSettingKey() => "Acc_Rpt_SabaPromotion_Promo_Sel";

    public override bool IsValid() => true;
}