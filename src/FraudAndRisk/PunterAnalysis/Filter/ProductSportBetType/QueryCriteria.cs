using Fanex.Data.Repository;

namespace PunterAnalysis.Filter.ProductSportBetType;

public class QueryCriteria : CriteriaBase
{
    public override string GetSettingKey() => "Punter-ProductSportBetTypeQuery";

    public override bool IsValid() => true;
}