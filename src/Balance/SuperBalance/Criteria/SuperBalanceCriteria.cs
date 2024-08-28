using Fanex.Data;
using Fanex.Data.Repository;

namespace SuperBalance.Criteria
{
    public class SuperBalanceCriteria : CriteriaBase
    {
        [SpParam(Ignored = true)]
        public int BalanceType { get; set; }

        [SpParam(SqlParamName = "Keyword")]
        public string Username { get; set; }

        [SpParam(SqlParamName = "FirstChar")]
        public string FirstLetter { get; set; }

        [SpParam(SqlParamName = "Currency")]
        public int? Currency { get; set; }

        [SpParam(SqlParamName = "IsViewTotalOnly")]
        public bool IsViewTotalOnly { get; set; }

        public decimal Total { get; set; }

        public override bool IsValid() => true;

        public override string GetSettingKey()
        {
            var storeProcedureName = "Balance_SuperBalance_Super";

            if (!string.IsNullOrEmpty(storeProcedureName) && BalanceType == 2)
            {
                storeProcedureName += "Yesterday";
            }

            return storeProcedureName;
        }
    }
}
