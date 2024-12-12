namespace HouseWinLoss.Soccer.MajorMinor
{
    using HouseWinLoss.Common.Criteria;
    using Configuration;

    public class Criteria : WinLossCriteria
    {
        public int IsLive { get; set; } = Constants.LiveDefault;

        public override string GetSettingKey() => "HouseWinlossByProducts_LvlN_Sportsbook_MajorMinor";

        public bool IsNoDraw { get; set; }

        public int IsSabaSport { get; set; } = Constants.IsSabaSportDefault;

        public int PermissionProfileID { get; set; }
    }
}