namespace HouseWinLoss.Common.Criteria
{
    public  class UniversalThirdPartyBetTypeCriteria : WinLossCriteria
    {
        public bool IsNoDraw { get; set; }

        public int AliasProductID { get; set; }

        public int PermissionProfileID { get; set; }

        public int IsLive { get; set; }

        public int SportType { get; set; }

        public override string GetSettingKey() => "HouseWinlossByProducts_LvlN_3rd_Bettype";

        public UniversalThirdPartyBetTypeCriteria Clone()
        {
            return new UniversalThirdPartyBetTypeCriteria
            {
                AliasProductID = AliasProductID,
                SportType = SportType,
                IsLive = IsLive,
                StartDate = StartDate,
                EndDate = EndDate,
                NormalMode = NormalMode,
                PermissionProfileID = PermissionProfileID
            };
        }
    }
}
