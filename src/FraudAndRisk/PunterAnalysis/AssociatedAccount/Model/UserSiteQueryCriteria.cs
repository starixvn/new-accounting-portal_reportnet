namespace PunterAnalysis.AssociatedAccount.Model;

using Fanex.Data.Repository;

public class UserSiteQueryCriteria : CriteriaBase
{
    public int CustId { get; set; }

    public override string GetSettingKey() => "Punter-UserSiteQuery";

    public override bool IsValid() => true;
}