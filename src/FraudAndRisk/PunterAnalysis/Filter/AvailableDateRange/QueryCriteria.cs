using Fanex.Data.Repository;

namespace PunterAnalysis.Filter.AvailableDateRange
{
    public class QueryCriteria : CriteriaBase
    {
        public override string GetSettingKey() => "Punter-AvailableDateRangeQuery";

        public override bool IsValid() => true;
    }
}