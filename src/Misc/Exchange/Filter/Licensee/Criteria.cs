using Fanex.Data.Repository;

namespace Exchange.Filter.Licensee;

public class Criteria : CriteriaBase
{
    public override string GetSettingKey() => "Exchange-LicenseeQuery";

    public override bool IsValid() => true;
}