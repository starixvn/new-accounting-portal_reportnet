namespace SMAWinLoss.Filter;

using Fanex.Data.Repository;

public class SiteCriteria : CriteriaBase
{
    public override string GetSettingKey() => "SMAWinLoss-SiteSel";

    public override bool IsValid() => true;
}