using Fanex.Data.Repository;

namespace LeagueWinLoss.Criteria
{
    public class VirtualRateCriteria : CriteriaBase
    {
        public int CustID { get; set; }

        public override string GetSettingKey()
        {
            return "WinLoss_LeagueWinLoss_GetVirtualRate";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}