namespace Nap_Reports.HouseWinLoss.Common.Criteria
{
    public class NumberGameDrilldownCriteria : WinLossCriteria
    {
        public bool IsNoDraw { get; set; }

        public int PermissionProfileID { get; set; }

        public int SportType { get; set; }

        public int LeagueId { get; set; }

        public int Bettype { get; set; }

        public string SiteType { get; set; }

        public override string GetSettingKey() => $"HouseWinlossByProducts_LvlN_NumberGame";
    }
}
