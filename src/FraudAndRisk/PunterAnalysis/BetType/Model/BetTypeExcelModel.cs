using Epplus.Mapper.Annotations;

namespace PunterAnalysis.BetType.Model
{
    public class BetTypeExcelModel
    {
        [Cell("A9")]
        public string BetTypeName { get; set; }

        [Cell("B9")]
        public decimal NonLiveWinLoss { get; set; }

        [Cell("C9")]
        public decimal NonLiveTurnover { get; set; }

        [Cell("D9")]
        public object NonLiveMargin => NonLiveTurnover == 0
            ? (object)"N/A" :
            (object)(NonLiveWinLoss / NonLiveTurnover);

        [Cell("E9")]
        public int NonLiveBetCount { get; set; }

        [Cell("F9")]
        public decimal LiveWinLoss { get; set; }

        [Cell("G9")]
        public decimal LiveTurnover { get; set; }

        [Cell("H9")]
        public object LiveMargin => LiveTurnover == 0
            ? (object)"N/A" :
            (object)(LiveWinLoss / LiveTurnover);

        [Cell("I9")]
        public int LiveBetCount { get; set; }

        [Cell("J9")]
        public decimal WinLoss { get; set; }

        [Cell("K9")]
        public decimal Turnover { get; set; }

        [Cell("L9")]
        public object Margin => Turnover == 0
            ? (object)"N/A" :
            (object)(WinLoss / Turnover);

        [Cell("M9")]
        public int BetCount { get; set; }
    }
}