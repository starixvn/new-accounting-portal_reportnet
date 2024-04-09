using Fanex.Data.Repository;

namespace ActualMemberBet.Filter
{
    public class OddsCriteria : CriteriaBase
    {
        public override string GetSettingKey() => "Actual-Member-Bet-Odds";

        public override bool IsValid() => true;
    }
}
