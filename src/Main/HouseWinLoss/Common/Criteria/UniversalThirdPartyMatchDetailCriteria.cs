namespace HouseWinLoss.Common.Criteria
{
    public  class UniversalThirdPartyMatchDetailCriteria : WinLossCriteria
    {
        public bool IsNoDraw { get; set; }

        public int PermissionProfileID { get; set; }

        public int IsLive { get; set; }

        public int SportType { get; set; }

        public int BetType { get; set; }

        public override string GetSettingKey() => "HouseWinlossByProducts_LvlN_3rd_MatchDetail";
    }
}
