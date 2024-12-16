namespace HouseWinLoss.CashOut.MatchDetail;

using Configuration;

public class QueryModel
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool NormalMode { get; set; } = true;

    public int IsLive { get; set; } = Constants.LiveDefault;

    public int SportType { get; set; }

    public int BetType { get; set; }

    public int PermissionProfileID { get; set; }
}