using PunterAnalysis.Shared;

namespace PunterAnalysis.League.Model;

public class Criteria : BetCountAndLeagueCriteriaBase
{
    public override string GetSettingKey() => "Punter-League";
}