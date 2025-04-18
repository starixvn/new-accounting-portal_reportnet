namespace PunterAnalysis.Filter.AvailableDatabase;

using Fanex.Data.Repository;

public class QueryCriteria : CriteriaBase
{
    public int TaskId => 1;

    public override string GetSettingKey() => "Punter-AvailabeDatabaseQuery";

    public override bool IsValid() => true;
}