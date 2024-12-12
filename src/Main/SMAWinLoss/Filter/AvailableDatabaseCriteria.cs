using Fanex.Data.Repository;
using SMAWinLoss.Constants;

namespace SMAWinLoss.Filter
{
    public class AvailableDatabaseCriteria : CriteriaBase
    {
        public int TaskId => TaskIdConstant.DailyTask;

        public override string GetSettingKey() => "SMAWinLoss-AvailabeDatabaseQuery";

        public override bool IsValid() => true;
    }
}