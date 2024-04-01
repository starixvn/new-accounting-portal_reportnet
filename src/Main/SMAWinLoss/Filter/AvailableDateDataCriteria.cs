using Fanex.Data.Repository;
using SMAWinLoss.Constants;

namespace SMAWinLoss.Filter
{
    public class AvailableDateDataCriteria : CriteriaBase
    {
        public AvailableDateDataCriteria()
        {
            TaskId = TaskIdConstant.DailyTask;
        }

        public int TaskId { get; }

        public override string GetSettingKey() => "SMAWinLoss-ReportLatestDate";

        public override bool IsValid() => true;
    }
}