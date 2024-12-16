using PunterAnalysis.Shared;

namespace PunterAnalysis.BetCount.Model;

public class Criteria : BetCountAndLeagueCriteriaBase
{
    public int BettypeGroupId { get; set; }

    public override string GetSettingKey() => "Punter-BetCount";
}