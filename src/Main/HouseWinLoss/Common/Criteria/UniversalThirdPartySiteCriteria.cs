namespace HouseWinLoss.Common.Criteria
{
    public  class UniversalThirdPartySiteCriteria : WinLossCriteria
    {
        public bool IsNoDraw { get; set; }

        public int PermissionProfileID { get; set; }

        public int IsLive { get; set; }

        public bool IsFreePlay { get; set; }

        public int SportType { get; set; }

        public int BetType { get; set; }

        public string SiteType { get; set; }

        public override string GetSettingKey() => $"HouseWinlossByProducts_LvlN_3rd_Site";
    }
}
