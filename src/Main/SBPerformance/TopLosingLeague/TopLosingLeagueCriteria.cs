using Fanex.Data.Repository;

namespace SBPerformance.TopLosingLeague
{
    public class TopLosingLeagueCriteria : CriteriaBase
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int TimeRange { get; set; }

        public decimal? Margin { get; set; }

        public string TopBy { get; set; }

        public bool ExcludeDrawTickets { get; set; }

        public bool ExcludeParlay { get; set; }

        public int? SiteID { get; set; }

        public int LeagueID { get; set; }

        public byte? LiveIndicator { get; set; }

        public string ShowBy { get; set; }

        public int? MatchID { get; set; }

        public int? BetType { get; set; }

        public override string GetSettingKey() => $"SBPerformanceMonitor-League";

        public override bool IsValid() => true;
    }
}