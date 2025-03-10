namespace HouseWinLoss.Soccer.LiveNonLive;

using Common.Criteria;

public class Criteria : WinLossCriteria
{
    public override string GetSettingKey() => "HouseWinlossByProducts_Lvl2";

    public bool IsNoDraw { get; set; }

    public int AliasProductID { get; set; }

    public int PermissionProfileID { get; set; }
}