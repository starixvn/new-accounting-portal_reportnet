namespace Exchange.Filter.Licensee;

using Fanex.Data.Repository;

public class Criteria : CriteriaBase
{
    public override string GetSettingKey() => "Exchange-LicenseeQuery";

    public override bool IsValid() => true;
}