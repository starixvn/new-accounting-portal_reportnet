namespace HouseWinLoss.Parlay.BetType;

public class QueryModel
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool NormalMode { get; set; } = true;

    public int AliasProductID { get; set; }

    public int PermissionProfileID => 1;

    public bool IsNoDraw { get; set; }
}