namespace DailyMixParlay.Criteria;

using Fanex.Data.Repository;

public class GetCombinationWinLossCriteria : CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int Site { get; set; }

    public override string GetSettingKey()
    {
        return "DailyMixParlayWinLoss_GetCombinationWinLoss";
    }

    public override bool IsValid()
    {
        return true;
    }
}