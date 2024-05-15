using Fanex.Data.Repository;

namespace PunterAnalysis.Filter.AvailableDatabase
{
    public class QueryCriteria : CriteriaBase
    {
        public QueryCriteria()
        {
            TaskId = 1;
        }

        public int TaskId { get; }

        public override string GetSettingKey() => "Punter-AvailabeDatabaseQuery";

        public override bool IsValid() => true;
    }
}