using Epplus.Mapper.Annotations;

namespace PunterAnalysis.Summary.Model
{
    public class SummaryExcelModel
    {
        [Cell("A8")]
        public string Username { get; set; }

        [Cell("B8")]
        public decimal WinLoss { get; set; }

        [Cell("C8")]
        public decimal Turnover { get; set; }

        [Cell("D8")]
        public object Margin => Turnover == 0
            ? "N/A" :
            WinLoss / Turnover;

        [Cell("E8")]
        public int BetCount { get; set; }

        [Cell("F8")]
        public string VIP { get; set; }

        [Cell("G8")]
        public string Category { get; set; }
    }
}