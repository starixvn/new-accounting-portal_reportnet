namespace LeagueWinLoss.Criteria;

using Fanex.Data.Repository;

public class PermissionCriteria : CriteriaBase
{
    public string FunctionKey { get; set; }

    public override string GetSettingKey() => "WinLoss_LeagueWinLoss_GetPermisionProfileID";

    public override bool IsValid() => true;
}