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

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            var storeProcedureName = string.Empty;
            switch (RoleID)
            {
                case RoleConstant.Master:
                    storeProcedureName = "Balance_DirectBalance_DirectMaster";
                    break;

                case RoleConstant.Agent:
                    storeProcedureName = "Balance_DirectBalance_DirectAgent";
                    break;

                case RoleConstant.Member:
                    storeProcedureName = "Balance_DirectBalance_DirectMember";
                    break;
            }

            if (!string.IsNullOrEmpty(storeProcedureName) && BalanceType == 2)
            {
                storeProcedureName += "Yesterday";
            }

            return storeProcedureName;
        }
    }
}
