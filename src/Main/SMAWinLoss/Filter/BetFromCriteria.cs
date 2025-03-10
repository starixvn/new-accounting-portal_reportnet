namespace SMAWinLoss.Filter;

using Fanex.Data.Repository;

public class BetFromCriteria : CriteriaBase
{
    public override string GetSettingKey() => "SMAWinLoss-BetFromSel";

    public override bool IsValid() => true;
}