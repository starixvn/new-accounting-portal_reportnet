using Fanex.Data.Repository;

namespace HouseWinLoss.Common.Criteria
{
    public class PermissionCriteria : CriteriaBase
    {
        public string FunctionKey { get; set; }

        public override string GetSettingKey() => "Permission-GetPermisionProfileID";

        public override bool IsValid() => true;
    }
}
