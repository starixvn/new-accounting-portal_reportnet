using Fanex.Data;
using Fanex.Data.Repository;

namespace PunterAnalysis.Filter.UsernameSuggestion
{
    public class QueryCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public string Username { get; set; }

        public int RoleId { get; set; }

        [SpParam(SqlParamName = "IsAllowFISearch")]
        public bool IsAllowFinanceUsersSearch => false;

        public override string GetSettingKey() => "Punter-UserNameSuggestion";

        public override bool IsValid() => true;
    }
}