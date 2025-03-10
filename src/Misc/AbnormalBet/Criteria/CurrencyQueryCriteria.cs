namespace AbnormalBet.Criteria;

using Fanex.Data.Repository;

public class CurrencyQueryCriteria : CriteriaBase
{
    public override bool IsValid() => true;

    public override string GetSettingKey() => "Acc_Rpt_AbnormalBet_CurrencySel";
}