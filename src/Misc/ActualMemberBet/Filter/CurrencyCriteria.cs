using Fanex.Data.Repository;

namespace ActualMemberBet.Filter
{
    public class CurrencyCriteria : CriteriaBase
    {
        public override string GetSettingKey() => "Actual-Member-Bet-Currency";

        public override bool IsValid() => true;
    }
}
