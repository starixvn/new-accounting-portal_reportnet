namespace SMAWinLoss.Filter;

using Fanex.Data.Repository;

public class LeagueGroupCriteria : CriteriaBase
{
    public override string GetSettingKey() => "SMAWinLoss-LeagueGroupSel";

    public override bool IsValid() => true;
}