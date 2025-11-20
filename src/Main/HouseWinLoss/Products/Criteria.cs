namespace HouseWinLoss.Products;

using Common.Criteria;

public class Criteria : WinLossCriteria
{
    public override string GetSettingKey() => "HouseWinlossByProducts_Lvl1";

    public bool IsNoDraw { get; set; }

    public bool ExcludeHedge { get; set; }

    public int PermissionProfileID { get; set; }
}