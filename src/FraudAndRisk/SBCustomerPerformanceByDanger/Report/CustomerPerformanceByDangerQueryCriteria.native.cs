using Fanex.Data.Repository;

namespace SBCustomerPerformanceByDanger.Report
{
    public class CustomerPerformanceByDangerQueryCriteria : CriteriaBase
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string Divisions { get; set; }

        public string DangerLevels { get; set; }

        public string SportBettypes { get; set; }

        public string LeagueName { get; set; }

        public string LeagueGroupIDs { get; set; }

        public string LiveStatus { get; set; }

        public int TopUsers { get; set; }

        public override string GetSettingKey() => $"Acc_Rpt_SBCustPerfByDangerLevel_WareHouse";

        public override bool IsValid() => true;
    }
}