namespace HouseWinLoss.Soccer.GroupBetType
{
    using Common.Criteria;
    using Configuration;

    public class GroupBetTypeCriteria : WinLossCriteria
    {
        public bool IsMajor { get; set; }

        public int IsLive { get; set; } = Constants.LiveDefault;

        public override string GetSettingKey() => "HouseWinlossByProducts_LvlN_Sportsbook_Bettype";

        public bool IsNoDraw { get; set; }

        public int IsSabaSport { get; set; } = Constants.IsSabaSportDefault;

        public int Sporttype { get; } = Constants.SportTypeSoccer;

        public int PermissionProfileID { get; set; }
    }
}