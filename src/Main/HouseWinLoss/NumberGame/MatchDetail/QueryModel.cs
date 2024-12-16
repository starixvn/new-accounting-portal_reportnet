namespace HouseWinLoss.NumberGame.MatchDetail;

public class QueryModel
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public bool NormalMode { get; set; } = true;

    public bool IsNoDraw { get; set; }

    public int PermissionProfileID { get; set; }

    public int SportType { get; set; }

    public int LeagueId { get; set; }

    public int Bettype { get; set; }

    public string SiteType { get; set; }
}