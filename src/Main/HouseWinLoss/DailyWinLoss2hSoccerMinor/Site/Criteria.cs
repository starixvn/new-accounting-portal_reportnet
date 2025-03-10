namespace HouseWinLoss.DailyWinLoss2hSoccerMinor.Site;

using Common.Criteria;
using Configuration;

public class Criteria : WinLossCriteria
{
    public int SportType { get; set; }

    public int BetType { get; set; }

    public string SiteType { get; set; }

    public int IsLive { get; set; } = Constants.LiveDefault;

    public int IsHedge { get; set; } = Constants.HedgeDefault;

    public int IsCorners { get; set; } = Constants.CornersDefault;

    public int IsSabaSport { get; set; } = Constants.IsAllSportDefault;

    public int PermissionProfileID { get; set; }

    public bool IsNoDraw { get; set; }

    public override string GetSettingKey() => "HouseWinLoss-Sportsbook-Site";
}