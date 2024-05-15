namespace PunterAnalysis.League.Model
{
    public class LeagueMappingModel
    {
        public int LeagueId { get; set; }

        public string LeagueName { get; set; }

        public int Year { get; set; }

        public int MonthOrWeek { get; set; }

        public decimal WinLoss { get; set; }

        public decimal Turnover { get; set; }

        public int BetCount { get; set; }
    }
}