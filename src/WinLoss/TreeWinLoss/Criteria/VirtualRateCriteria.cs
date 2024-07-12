namespace TreeWinLoss.Criteria
{
    using Fanex.Data.Repository;

    public class VirtualRateCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "WinLoss_TreeWinLoss_GetVirtualRate";
        }
    }
}
