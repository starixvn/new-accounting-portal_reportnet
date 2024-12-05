
using Epplus.Mapper.Annotations;

namespace Margin.Model
{
    public class MarginWinLossExcelModel
    {

        [Cell("A4")]
        public int No { get; set; }

        [Cell("B4")]
        public string UserName { get; set; }

        public int BettypeId { get; set; }

        [Cell("C4")]
        public string BettypeName { get; set; }

        [Cell("D4")]
        public decimal WinningAmount { get; set; }

        [Cell("E4")]
        public decimal Turnover { get; set; }

        [Cell("F4")]
        public decimal Ratio { get; set; }

        [Cell("G4")]
        public int TotalBet { get; set; }
    }
}
