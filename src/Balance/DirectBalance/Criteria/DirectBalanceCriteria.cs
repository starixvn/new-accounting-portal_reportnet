using DirectBalance.Constant;
using Fanex.Data;
using Fanex.Data.Repository;

namespace DirectBalance.Criteria
{
    public class DirectBalanceCriteria : CriteriaBase
    {
        [SpParam(Ignored = true)]
        public int RoleID { get; set; }

        [SpParam(Ignored = true)]
        public int BalanceType { get; set; }

        public int? Currency { get; set; }

        public string Username { get; set; }

        public bool ExclTestCurr { get; set; }

        public override bool IsValid() => true;

        public override string GetSettingKey()
        {
            var storeProcedureName = RoleID switch
            {
                RoleConstant.Master => "Balance_DirectBalance_DirectMaster",
                RoleConstant.Agent => "Balance_DirectBalance_DirectAgent",
                RoleConstant.Member => "Balance_DirectBalance_DirectMember",
                _ => string.Empty
            };

            if (!string.IsNullOrEmpty(storeProcedureName) && BalanceType == 2)
            {
                storeProcedureName += "Yesterday";
            }

            return storeProcedureName;
        }
    }
}
