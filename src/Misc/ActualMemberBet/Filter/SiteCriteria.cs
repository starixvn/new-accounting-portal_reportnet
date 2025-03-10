namespace ActualMemberBet.Filter;

using Fanex.Data.Repository;

public class SiteCriteria : CriteriaBase
{
    public override string GetSettingKey() => "Actual-Member-Bet-Site";

    public override bool IsValid() => true;
}