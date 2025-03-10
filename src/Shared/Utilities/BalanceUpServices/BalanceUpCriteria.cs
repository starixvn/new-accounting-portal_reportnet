namespace Utilities.BalanceUpServices;

using Fanex.Data.Repository;

public class BalanceUpCriteria : CriteriaBase
{
    public override string GetSettingKey() => "GetBalanceUpRange";

    public override bool IsValid() => true;
}