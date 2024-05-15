namespace PunterAnalysis.Performance.Summary.Model
{
    public class BetTypeStatistic
    {
        public string BetTypeName { get; set; }

        public int BetCount { get; set; }

        public decimal WinLoss { get; set; }

        public decimal Turnover { get; set; }

        public decimal Margin { get; set; }
    }
}