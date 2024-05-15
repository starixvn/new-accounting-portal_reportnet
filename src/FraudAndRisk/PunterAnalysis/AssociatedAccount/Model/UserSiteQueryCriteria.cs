using Fanex.Data.Repository;

namespace PunterAnalysis.AssociatedAccount.Model
{
    public class UserSiteQueryCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public override string GetSettingKey() => "Punter-UserSiteQuery";

        public override bool IsValid() => true;
    }
}