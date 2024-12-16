namespace PunterAnalysis.League.Model;

public class LeagueRawModel
{
    public int LeagueId { get; set; }

    public string LeagueName { get; set; }

    public int Year { get; set; }

    public int Month { get; set; }

    public int Week { get; set; }

    public decimal WinLoss { get; set; }

    public decimal Turnover { get; set; }

    public int BetCount { get; set; }
}