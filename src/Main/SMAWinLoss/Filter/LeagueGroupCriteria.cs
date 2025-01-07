using Fanex.Data.Repository;

namespace SMAWinLoss.Filter;

public class LeagueGroupCriteria : CriteriaBase
{
    public override string GetSettingKey() => "SMAWinLoss-LeagueGroupSel";

    public override bool IsValid() => true;
}