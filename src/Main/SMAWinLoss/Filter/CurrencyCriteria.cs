using Fanex.Data.Repository;

namespace SMAWinLoss.Filter
{
    public class CurrencyCriteria : CriteriaBase
    {
        public override string GetSettingKey() => $"SMAWinLoss-CurrencySel";

        public override bool IsValid() => true;
    }
}