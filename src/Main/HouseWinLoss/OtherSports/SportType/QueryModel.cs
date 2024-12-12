namespace HouseWinLoss.OtherSports.SportType
{
    using Configuration;

    public class QueryModel
    {
        public int IsLive { get; set; } = Constants.LiveDefault;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool NormalMode { get; set; } = true;

        public bool IsNoDraw { get; set; }

        public int PermissionProfileID { get; set; }
    }
}