namespace PunterAnalysis.SportType.Model
{
    public class SportTypeModel
    {
        public int SportId { get; set; }

        public string SportName { get; set; }

        public decimal WinLoss { get; set; }

        public decimal Turnover { get; set; }

        public decimal Margin => Turnover == 0 ? 0 : WinLoss / Turnover;

        public int BetCount { get; set; }

        public int LeagueGroupId { get; set; }

        public static SportTypeModel operator +(SportTypeModel a, SportTypeModel b)
        {
            return new SportTypeModel
            {
                SportId = 0,
                SportName = string.Empty,
                WinLoss = a.WinLoss + b.WinLoss,
                BetCount = a.BetCount + b.BetCount,
                Turnover = a.Turnover + b.Turnover,
                LeagueGroupId = 0
            };
        }
    }
}