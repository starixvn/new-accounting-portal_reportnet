namespace Exchange.Filter.Sport;

using Fanex.Data.Repository;

public class Criteria : CriteriaBase
{
    public override string GetSettingKey() => "Exchange-SportQuery";

    public override bool IsValid() => true;
}