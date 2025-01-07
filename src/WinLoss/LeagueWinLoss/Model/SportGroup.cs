namespace LeagueWinLoss.Model;

public class SportGroup
{
    public int GroupId { get; set; }

    public string GroupName { get; set; }

    public List<Sport> ChildItems { get; set; }
}