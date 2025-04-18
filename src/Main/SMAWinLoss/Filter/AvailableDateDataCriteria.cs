namespace SMAWinLoss.Filter;

using Constants;
using Fanex.Data.Repository;

public class AvailableDateDataCriteria : CriteriaBase
{
    public int TaskId => TaskIdConstant.DailyTask;

    public override string GetSettingKey() => "SMAWinLoss-ReportLatestDate";

    public override bool IsValid() => true;
}