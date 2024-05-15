using Fanex.Data.Repository;

namespace PunterAnalysis.BetTypeGroup.Model
{
    public class Criteria : CriteriaBase
    {
        public int PeriodType { get; set; } = 1;

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int CustId { get; set; }

        public string ListCombinationId { get; set; }

        public int OrderBy { get; set; } = 1;

        public int SortBy { get; set; } = 1;

        public int LeagueGroupId { get; set; }

        public override string GetSettingKey() => "Punter-BetTypeGroup";

        public override bool IsValid() => true;
    }
}