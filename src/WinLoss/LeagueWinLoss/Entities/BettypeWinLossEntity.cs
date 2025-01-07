namespace LeagueWinLoss.Entities;

public class BettypeWinLossEntity : BaseEntity
{
    public int Bettype { get; set; }

    public string BettypeName { get; set; }

    public string SiteType { get; set; } = string.Empty;

    public bool IsHedge { get; set; }

    public int TraderID { get; set; }

    public string TraderName { get; set; }

    public string Choice { get; set; }
}