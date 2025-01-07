using Fanex.Data;
using Fanex.Data.Repository;

namespace DailyMixParlay.Criteria;

public class GetAdvanceMixParlayCriteria : CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int Site { get; set; }

    [SpParam(Ignored = true)]
    public int Bettype => 9;

    [SpParam(Ignored = true)]
    public string BetName { get; set; }

    public override string GetSettingKey()
    {
        return "DailyMixParlayWinLoss_GetAdvanceMixParlay";
    }

    public override bool IsValid()
    {
        return true;
    }
}