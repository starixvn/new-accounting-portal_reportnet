using Fanex.Data.Repository;

namespace Utilities.Currencies;

public class CurrenciesCriteria : CriteriaBase
{
    public bool ExcludeTest { get; set; } = false;

    public override string GetSettingKey()
    {
        return "NAP_ExchangeCurrencySel";
    }

    public override bool IsValid()
    {
        return true;
    }
}