namespace HouseWinLoss.OtherSports.BetType;

using Common.Criteria;
using Configuration;

public class Criteria : WinLossCriteria
{
    public int SportType { get; set; }

    public int IsLive { get; set; } = Constants.LiveDefault;

    public bool IsNoDraw { get; set; }

    public int IsSabaSport { get; set; } = Constants.IsSabaSportDefault;

    public int PermissionProfileID { get; set; }

    public override string GetSettingKey() => "HouseWinlossByProducts_LvlN_Sportsbook_Bettype";
}