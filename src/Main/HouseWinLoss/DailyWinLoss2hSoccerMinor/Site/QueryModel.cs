namespace HouseWinLoss.DailyWinLoss2hSoccerMinor.Site
{
    using HouseWinLoss.Configuration;

    public class QueryModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool NormalMode { get; set; } = true;

        public int SportType { get; set; } = Constants.SportTypeSoccer;

        public int BetType { get; set; }

        public string SiteType { get; set; }

        public int IsLive { get; set; } = Constants.LiveDefault;

        public int IsHedge { get; set; } = Constants.HedgeDefault;

        public int IsCorners { get; set; } = Constants.CornersDefault;

        public int IsSabaSport { get; set; } = Constants.IsAllSportDefault;

        public int PermissionProfileID { get; set; }

        public bool IsNoDraw { get; set; }
    }
}