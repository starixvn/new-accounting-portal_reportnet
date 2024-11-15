namespace HouseWinLoss.Soccer.BetType
{
    using HouseWinLoss.Common.Criteria;
    using HouseWinLoss.Configuration;

    public class BetTypeCriteria : WinLossCriteria
    {
        public int IsMajor { get; set; }

        public int IsLive { get; set; } = Constants.LiveDefault;

        public override string GetSettingKey() => $"HouseWinlossByProducts_LvlN_Sportsbook_Bettype";

        public bool IsNoDraw { get; set; }

        public int IsSabaSport { get; set; } = Constants.IsSabaSportDefault;

        public int Sporttype { get; } = Constants.SportTypeSoccer;

        public int PermissionProfileID { get; set; }
    }
}