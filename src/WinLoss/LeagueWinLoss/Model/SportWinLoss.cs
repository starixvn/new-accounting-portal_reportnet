namespace LeagueWinLoss.Model;

[Serializable]
public class SportWinLoss
{
    public string SportName { get; set; }

    public List<LeagueWinLossItem> Leagues { get; set; }

    public LeagueWinLossItem Total { get; set; }
}