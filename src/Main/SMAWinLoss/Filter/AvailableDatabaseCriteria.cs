namespace SMAWinLoss.Filter;

using Constants;
using Fanex.Data.Repository;

public class AvailableDatabaseCriteria : CriteriaBase
{
    public int TaskId => TaskIdConstant.DailyTask;

    public override string GetSettingKey() => "SMAWinLoss-AvailabeDatabaseQuery";

    public override bool IsValid() => true;
}