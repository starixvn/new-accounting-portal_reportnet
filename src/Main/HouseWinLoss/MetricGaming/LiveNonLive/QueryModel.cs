namespace HouseWinLoss.MetricGaming.LiveNonLive;

public class QueryModel
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool NormalMode { get; set; } = true;

    public bool IsNoDraw { get; set; }

    public bool ExcludeHedge { get; set; }

    public int AliasProductID { get; set; }

    public int PermissionProfileID { get; set; }
}