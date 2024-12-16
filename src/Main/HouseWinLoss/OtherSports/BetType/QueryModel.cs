namespace HouseWinLoss.OtherSports.BetType;

using Configuration;

public class QueryModel
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool NormalMode { get; set; } = true;

    public int SportType { get; set; }

    public int IsLive { get; set; } = Constants.LiveDefault;

    public bool IsNoDraw { get; set; }

    public int IsSabaSport { get; set; } = Constants.IsSabaSportDefault;

    public int PermissionProfileID { get; set; }
}