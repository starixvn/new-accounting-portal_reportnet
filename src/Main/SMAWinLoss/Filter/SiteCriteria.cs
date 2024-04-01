using Fanex.Data.Repository;

namespace SMAWinLoss.Filter
{
    public class SiteCriteria : CriteriaBase
    {
        public override string GetSettingKey() => $"SMAWinLoss-SiteSel";

        public override bool IsValid() => true;
    }
}