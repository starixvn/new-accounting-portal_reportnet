namespace PunterAnalysis.Filter.ProductSportBetType;

using Fanex.Data.Repository;

public class QueryCriteria : CriteriaBase
{
    public override string GetSettingKey() => "Punter-ProductSportBetTypeQuery";

    public override bool IsValid() => true;
}