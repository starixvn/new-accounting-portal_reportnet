namespace PunterAnalysis.Filter.AvailableDateRange;

using Fanex.Data.Repository;

public class QueryCriteria : CriteriaBase
{
    public override string GetSettingKey() => "Punter-AvailableDateRangeQuery";

    public override bool IsValid() => true;
}