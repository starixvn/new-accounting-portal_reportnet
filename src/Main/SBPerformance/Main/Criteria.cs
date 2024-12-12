using Fanex.Data.Repository;

namespace SBPerformance.Main
{
    public class Criteria : CriteriaBase
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int TimeRange { get; set; }

        public bool IsShowSite { get; set; }

        public bool IsShowSport { get; set; }

        public bool IsShowLeague { get; set; }

        public bool IsShowCust { get; set; }

        public decimal? Margin { get; set; }

        public int Top { get; set; }

        public string TopBy { get; set; }

        public bool ExcludeDrawTickets { get; set; }

        public bool ExcludeParlay { get; set; }

        public override string GetSettingKey() => "SBPerformanceMonitor-Dashboard";

        public override bool IsValid() => true;
    }
}