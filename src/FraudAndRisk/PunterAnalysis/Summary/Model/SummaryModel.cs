namespace PunterAnalysis.Summary.Model
{
    public class SummaryModel
    {
        public int CustId { get; set; }

        public string Username { get; set; }

        public decimal WinLoss { get; set; }

        public decimal Turnover { get; set; }

        public decimal Margin => Turnover == 0 ? 0 : WinLoss / Turnover;

        public int BetCount { get; set; }

        public string VIP { get; set; }

        public string Category { get; set; }

        public static SummaryModel operator +(SummaryModel a, SummaryModel b)
        {
            return new SummaryModel
            {
                Username = string.Empty,
                WinLoss = a.WinLoss + b.WinLoss,
                BetCount = a.BetCount + b.BetCount,
                Turnover = a.Turnover + b.Turnover
            };
        }
    }
}