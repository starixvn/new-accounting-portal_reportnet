using Nap_Reports.HouseWinLoss.Common.Criteria;

namespace Nap_Reports.HouseWinLoss.Parlay.BetType
{
    public class Criteria : WinLossCriteria
    {
        public int AliasProductID { get; set; }

        public int PermissionProfileID { get; set; }

        public bool IsNoDraw { get; set; }

        public override string GetSettingKey() => $"HouseWinlossByProducts_Lvl2";
    }
}