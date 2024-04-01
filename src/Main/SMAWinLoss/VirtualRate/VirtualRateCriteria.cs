using Fanex.Data.Repository;

namespace UniversalReports.SMAWinLoss.VirtualRate
{
    public class VirtualRateCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public override string GetSettingKey() => $"SMAWinLoss-VirtualRate";

        public override bool IsValid() => true;
    }
}