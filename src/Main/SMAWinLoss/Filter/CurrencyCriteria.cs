namespace SMAWinLoss.Filter;

using Fanex.Data.Repository;

public class CurrencyCriteria : CriteriaBase
{
    public override string GetSettingKey() => "SMAWinLoss-CurrencySel";

    public override bool IsValid() => true;
}