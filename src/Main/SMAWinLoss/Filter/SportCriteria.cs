using Fanex.Data.Repository;

namespace SMAWinLoss.Filter
{
    public class SportCriteria : CriteriaBase
    {
        public override string GetSettingKey() => "SMAWinLoss-SportSel";

        public override bool IsValid() => true;
    }
}