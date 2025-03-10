namespace PunterAnalysis.BetCount.Model;

using Shared;

public class Criteria : BetCountAndLeagueCriteriaBase
{
    public int BettypeGroupId { get; set; }

    public override string GetSettingKey() => "Punter-BetCount";
}