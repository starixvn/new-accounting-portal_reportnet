using HouseWinLoss.Common.Criteria;

namespace HouseWinLoss.Products;

public class Criteria : WinLossCriteria
{
    public override string GetSettingKey() => "HouseWinlossByProducts_Lvl1";

    public bool IsNoDraw { get; set; }

    public int PermissionProfileID { get; set; }
}