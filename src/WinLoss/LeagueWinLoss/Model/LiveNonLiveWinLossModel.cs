namespace LeagueWinLoss.Model;

public class LiveNonLiveWinLossModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public BettypeWinLossModel BetTypeWinLoss { get; set; }
}