namespace PunterAnalysis.BetType.Model
{
    public class BetTypeModel
    {
        public int BetTypeId { get; set; }

        public string BetTypeName { get; set; }

        public bool HasLeague { get; set; }

        public decimal NonLiveWinLoss { get; set; }

        public decimal NonLiveTurnover { get; set; }

        public decimal NonLiveMargin => NonLiveTurnover == 0 ? 0 : NonLiveWinLoss / NonLiveTurnover;

        public int NonLiveBetCount { get; set; }

        public decimal LiveWinLoss { get; set; }

        public decimal LiveTurnover { get; set; }

        public decimal LiveMargin => LiveTurnover == 0 ? 0 : LiveWinLoss / LiveTurnover;

        public int LiveBetCount { get; set; }

        public decimal WinLoss => LiveWinLoss + NonLiveWinLoss;

        public decimal Turnover => LiveTurnover + NonLiveTurnover;

        public decimal Margin => Turnover == 0 ? 0 : WinLoss / Turnover;

        public int BetCount => LiveBetCount + NonLiveBetCount;

        public static BetTypeModel operator +(BetTypeModel a, BetTypeModel b)
        {
            return new BetTypeModel
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
}