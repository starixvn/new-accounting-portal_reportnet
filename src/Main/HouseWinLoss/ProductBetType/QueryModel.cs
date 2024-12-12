namespace HouseWinLoss.ProductBetType
{
    using Configuration;

    public class QueryModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool NormalMode { get; set; } = true;

        public int PermissionProfileID { get; set; }

        public bool IsNoDraw { get; set; }

        public int IsMajor { get; set; }

        public int IsLive { get; set; } = Constants.LiveDefault;

        public int IsSabaSport { get; set; } = Constants.IsSabaSportDefault;

        public int AliasProductID { get; set; }

        public int SportType { get; set; }

        public string SiteType { get; set; }
    }
}
