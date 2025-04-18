namespace SabaPromotion.Criteria;

using Fanex.Data.Repository;

public class SabaPromotionCriteria: CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string BetTypeIds { get; set; }

    public bool IsTest { get; set; }

    public override string GetSettingKey() => "Acc_Rpt_SabaPromotion";

    public override bool IsValid() => true;
}