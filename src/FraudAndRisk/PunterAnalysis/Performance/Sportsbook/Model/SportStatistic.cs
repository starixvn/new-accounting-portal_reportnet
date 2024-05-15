namespace PunterAnalysis.Performance.Sportsbook.Model
{
    public class SportStatistic
    {
        public string SportName { get; set; }

        public int BetCount { get; set; }

        public decimal WinLoss { get; set; }

        public decimal Turnover { get; set; }
    }
}