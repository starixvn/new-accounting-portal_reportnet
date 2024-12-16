using Fanex.Data.Repository;

namespace Nap_Reports.Sites.Criteria
{
    public class PermissionCriteria : CriteriaBase
    {
        public string FunctionKey { get; set; }

        public override string GetSettingKey() => "Permission-GetPermisionProfileID";

        public override bool IsValid() => true;
    }
}
