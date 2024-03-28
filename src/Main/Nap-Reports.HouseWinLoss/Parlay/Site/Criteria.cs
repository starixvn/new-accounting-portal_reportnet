using Nap_Reports.HouseWinLoss.Common.Criteria;

namespace Nap_Reports.HouseWinLoss.Parlay.Site
{
    public class Criteria : WinLossCriteria
    {
        public string SiteType { get; set; }

        public int PermissionProfileID { get; set; }

        public override string GetSettingKey() => $"HouseWinlossByProducts_LvlN_Parlay";
    }
}