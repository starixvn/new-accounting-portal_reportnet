using Fanex.Data.Repository;
using SMAWinLoss.Constants;

namespace SMAWinLoss.Filter
{
    public class AvailableDatabaseCriteria : CriteriaBase
    {
        public AvailableDatabaseCriteria()
        {
            TaskId = TaskIdConstant.DailyTask;
        }

        public int TaskId { get; }

        public override string GetSettingKey() => "SMAWinLoss-AvailabeDatabaseQuery";

        public override bool IsValid() => true;
    }
}