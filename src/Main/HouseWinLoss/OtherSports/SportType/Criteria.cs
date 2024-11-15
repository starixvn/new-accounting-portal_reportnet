namespace HouseWinLoss.OtherSports.SportType
{
    using HouseWinLoss.Common.Criteria;
    using HouseWinLoss.Configuration;

    public class Criteria : WinLossCriteria
    {
        public int IsLive { get; set; } = Constants.LiveDefault;

        public bool IsNoDraw { get; set; }

        public int PermissionProfileID { get; set; }

        public override string GetSettingKey() => $"HouseWinlossByProducts_LvlN_Sportsbook_Sporttype";
    }
}