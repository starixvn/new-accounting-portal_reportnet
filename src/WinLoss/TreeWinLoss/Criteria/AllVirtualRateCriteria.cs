namespace TreeWinLoss.Criteria;

using Fanex.Data.Repository;

public class AllVirtualRateCriteria : CriteriaBase
{
    public string CustIDs { get; set; }

    public override bool IsValid()
    {
        return true;
    }

    public override string GetSettingKey()
    {
        return "WinLoss_TreeWinLoss_GetAllVirtualRate";
    }
}