namespace HouseWinLoss.Common.Criteria
{
    public class UniversalSecondLevelCriteria : WinLossCriteria
    {
        public bool IsNoDraw { get; set; }

        public int AliasProductID { get; set; }

        public int PermissionProfileID { get; set; }

        public override string GetSettingKey() => "HouseWinlossByProducts_Lvl2";
    }
}
