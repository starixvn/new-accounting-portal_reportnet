namespace LeagueWinLoss.Criteria;

using Fanex.Data.Repository;

public class SportCriteria : CriteriaBase
{
    public override string GetSettingKey()
    {
        return "WinLoss_LeagueWinLoss_GetSports";
    }

    public override bool IsValid()
    {
        return true;
    }
}