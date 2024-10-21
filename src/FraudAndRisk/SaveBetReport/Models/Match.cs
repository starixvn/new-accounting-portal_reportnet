namespace SaveBetReport.Models;

public class Match
{
    public int MatchId { get; set; }

    public string MatchCode { get; set; }

    public string Home { get; set; }

    public string Away { get; set; }

    public DateTime EventDate { get; set; }

    public string EventDateString { get; set; }

    public int SportType { get; set; }
}