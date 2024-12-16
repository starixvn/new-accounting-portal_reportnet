namespace LeagueWinLoss.Model;

public class Sport
{
    public int SportType { get; set; }

    public string SportName { get; set; }

    public List<BetTypeGroup> BettypeGroups { get; set; }
}