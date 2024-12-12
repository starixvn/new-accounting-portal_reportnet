namespace HouseWinLoss.Parlay.Site
{
    using Configuration;

    public class QueryModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool NormalMode { get; set; } = true;

        public string SiteType { get; set; } = Constants.SiteDefault;

        public int PermissionProfileID { get; set; } 
    }
}