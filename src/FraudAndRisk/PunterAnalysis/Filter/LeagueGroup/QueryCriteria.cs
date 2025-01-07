using Fanex.Data.Repository;

namespace PunterAnalysis.Filter.LeagueGroup;

public class QueryCriteria : CriteriaBase
{
    public override string GetSettingKey() => "Punter-LeagueGroupQuery";

    public override bool IsValid() => true;
}