namespace SMAWinLoss.Filter;

using Fanex.Data.Repository;

public class SportCriteria : CriteriaBase
{
    public override string GetSettingKey() => "SMAWinLoss-SportSel";

    public override bool IsValid() => true;
}