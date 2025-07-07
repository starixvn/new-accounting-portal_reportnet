namespace LeagueWinLoss.Model;

using System.Collections.Generic;

public class LeagueGroupGroup
{
    public string SportName { get; set; }
    public int SportType { get; set; }
    public List<LeagueGroupItem> LeagueGroups { get; set; }
}

public class LeagueGroupItem
{
    public string GroupId { get; set; }
    public string GroupName { get; set; }
    public string SportName { get; set; }
} 