namespace TreeWinLoss.Criteria;

using Fanex.Data.Repository;

public class SportCriteria : CriteriaBase
{
    public override bool IsValid()
    {
        return true;
    }

    public override string GetSettingKey()
    {
        return "WinLoss_TreeWinLoss_GetSport";
    }
}