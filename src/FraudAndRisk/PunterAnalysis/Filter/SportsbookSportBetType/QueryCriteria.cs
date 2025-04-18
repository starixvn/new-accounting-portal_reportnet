namespace PunterAnalysis.Filter.SportsbookSportBetType;

using Fanex.Data.Repository;

public class QueryCriteria : CriteriaBase
{
    public override string GetSettingKey() => "Punter-SportsbookSportBetTypeQuery";

    public override bool IsValid() => true;
}