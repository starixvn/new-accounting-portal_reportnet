using Fanex.Data.Repository;
using SMAWinLoss.Constants;

namespace SMAWinLoss.Filter
{
    public class AvailableDateDataCriteria : CriteriaBase
    {
        public int TaskId { get; } = TaskIdConstant.DailyTask;

        public override string GetSettingKey() => "SMAWinLoss-ReportLatestDate";

        public override bool IsValid() => true;
    }
}