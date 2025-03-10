namespace HouseWinLoss.MetricGaming.Site;

using Configuration;

public class QueryModel
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool NormalMode { get; set; } = true;

    public bool IsNoDraw { get; set; }

    public int PermissionProfileID { get; set; }

    public int IsLive { get; set; } = Constants.LiveDefault;

    public bool IsFreePlay { get; set; }

    public int SportType { get; set; }

    public int BetType { get; set; }

    public string SiteType { get; set; }
}