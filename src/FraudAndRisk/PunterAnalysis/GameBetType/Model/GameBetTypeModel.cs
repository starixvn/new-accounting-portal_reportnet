using PunterAnalysis.BetType.Model;

namespace PunterAnalysis.GameBetType.Model;

public class GameBetTypeModel : BetTypeModel
{
    public static GameBetTypeModel operator +(GameBetTypeModel a, GameBetTypeModel b)
    {
        return new GameBetTypeModel
        {
            BetTypeId = 0,
            BetTypeName = string.Empty,
            HasLeague = false,
            LiveBetCount = a.LiveBetCount + b.LiveBetCount,
            LiveWinLoss = a.LiveWinLoss + b.LiveWinLoss,
            LiveTurnover = a.LiveTurnover + b.LiveTurnover,
            NonLiveBetCount = a.NonLiveBetCount + b.NonLiveBetCount,
            NonLiveWinLoss = a.NonLiveWinLoss + b.NonLiveWinLoss,
            NonLiveTurnover = a.NonLiveTurnover + b.NonLiveTurnover
        };
    }
}