namespace PunterAnalysis.Filter.LeagueGroup;

using Fanex.Data.Repository;

public class QueryCriteria : CriteriaBase
{
    public override string GetSettingKey() => "Punter-LeagueGroupQuery";

    public override bool IsValid() => true;
}