namespace TreeWinLoss.Criteria;

using Fanex.Data.Repository;

public class VirtualRateHistoryCriteria : CriteriaBase
{
    public int CustId { get; set; }

    public override bool IsValid()
    {
        return true;
    }

    public override string GetSettingKey()
    {
        return "WinLoss_TreeWinLoss_VirtualRateHistory";
    }
}