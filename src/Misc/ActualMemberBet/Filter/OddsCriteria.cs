namespace ActualMemberBet.Filter;

using Fanex.Data.Repository;

public class OddsCriteria : CriteriaBase
{
    public override string GetSettingKey() => "Actual-Member-Bet-Odds";

    public override bool IsValid() => true;
}