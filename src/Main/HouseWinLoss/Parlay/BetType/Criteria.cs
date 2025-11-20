namespace HouseWinLoss.Parlay.BetType;

using Common.Criteria;

public class Criteria : WinLossCriteria
{
    public int AliasProductID { get; set; }

    public int PermissionProfileID { get; set; }

    public bool IsNoDraw { get; set; }

    public bool ExcludeHedge { get; set; }

    public override string GetSettingKey() => "HouseWinlossByProducts_Lvl2";
}