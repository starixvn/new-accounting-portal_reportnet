namespace PunterAnalysis.AssociatedAccount.Model
{
    public class AssociatedSummaryModel
    {
        public int CustId { get; set; }

        public string Username { get; set; }

        public string RoleName { get; set; }

        public decimal WinLoss { get; set; }

        public decimal Turnover { get; set; }

        public decimal Margin => Turnover == 0 ? 0 : WinLoss / Turnover;

        public int BetCount { get; set; }

        public string Category { get; set; }

        public static AssociatedSummaryModel operator +(AssociatedSummaryModel a, AssociatedSummaryModel b)
        {
            return new AssociatedSummaryModel
            {
                Username = string.Empty,
                RoleName = string.Empty,
                WinLoss = a.WinLoss + b.WinLoss,
                BetCount = a.BetCount + b.BetCount,
                Turnover = a.Turnover + b.Turnover
            };
        }
    }
}