using Fanex.Data.Repository;

namespace SabaPromotion.Criteria;

public class PromotionTypeCriteria: CriteriaBase
{
    public override string GetSettingKey() => "Acc_Rpt_SabaPromotion_Promo_Sel";

    public override bool IsValid() => true;
}