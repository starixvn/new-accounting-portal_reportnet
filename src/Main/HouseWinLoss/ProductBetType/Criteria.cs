namespace HouseWinLoss.ProductBetType;

using HouseWinLoss.Common.Criteria;
using Configuration;

public class Criteria : WinLossCriteria
{
    public int SportType { get; set; }

    public string SiteType { get; set; }

    public int IsLive { get; set; } = Constants.LiveDefault;

    public bool IsNoDraw { get; set; }

    public int IsSabaSport { get; set; } = Constants.IsSabaSportDefault;

    public int AliasProductID { get; set; }

    public int PermissionProfileID { get; set; }

    public int IsMajor { get; set; }

    public override string GetSettingKey() => "HouseWinlossByProducts_Lvl2_BettypePerformance";
}