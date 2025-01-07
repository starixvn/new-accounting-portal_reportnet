namespace PunterAnalysis.Game.Model;

public class GameModel
{
    public int LeagueGroupId { get; set; }

    public string LeagueGroupName { get; set; }

    public decimal WinLoss { get; set; }

    public decimal Turnover { get; set; }

    public decimal Margin => Turnover == 0 ? 0 : WinLoss / Turnover;

    public int BetCount { get; set; }

    public static GameModel operator +(GameModel a, GameModel b)
    {
        return new GameModel
        {
            LeagueGroupId = 0,
            LeagueGroupName = string.Empty,
            WinLoss = a.WinLoss + b.WinLoss,
            BetCount = a.BetCount + b.BetCount,
            Turnover = a.Turnover + b.Turnover
        };
    }
}