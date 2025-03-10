namespace HouseWinLoss.Parlay.Site;

using Common.Criteria;

public class Criteria : WinLossCriteria
{
    public string SiteType { get; set; }

    public int PermissionProfileID { get; set; }

    public override string GetSettingKey() => "HouseWinlossByProducts_LvlN_Parlay";
}