namespace ResultAnalysis.Models;

public class LeagueModel
{
    public int GroupId { get; set; }

    public string GroupName { get; set; }

    public int LeagueId { get; set; }

    public string LeagueName { get; set; }

    public int LengthOfLeagueName => LeagueName.Length;
}