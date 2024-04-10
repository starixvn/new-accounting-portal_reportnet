using HouseWinLoss.Configuration;

namespace HouseWinLoss.MetricGaming.BetType
{
    public class QueryModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool NormalMode { get; set; } = true;

        public bool IsNoDraw { get; set; }

        public int AliasProductID { get; set; }

        public int PermissionProfileID { get; set; }

        public int IsLive { get; set; } = Constants.LiveDefault;

        public int SportType { get; set; }
    }
}