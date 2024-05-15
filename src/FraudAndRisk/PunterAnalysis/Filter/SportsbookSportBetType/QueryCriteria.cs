using Fanex.Data.Repository;

namespace PunterAnalysis.Filter.SportsbookSportBetType
{
    public class QueryCriteria : CriteriaBase
    {
        public override string GetSettingKey() => "Punter-SportsbookSportBetTypeQuery";

        public override bool IsValid() => true;
    }
}