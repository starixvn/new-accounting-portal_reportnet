namespace BetCount.Model
{
    public class TransactionModel
    {
        public string SiteName { get; set; }

        public int BetCount { get; set; }

        public decimal Turnover { get; set; }

        public decimal MemberWinLoss { get; set; }

        public decimal MarginPercent => Turnover == 0 ? 0 : MemberWinLoss / Turnover * 100;

        public decimal AgentWinLoss { get; set; }

        public decimal MasterWinLoss { get; set; }

        public decimal SuperWinLoss { get; set; }

        public decimal CompanyWinLoss { get; set; }

        public decimal TotalWinLoss { get; set; }

        public decimal Commission { get; set; }

        public decimal ExtraPT { get; set; }

        public static TransactionModel operator +(TransactionModel a, TransactionModel b)
        {
            return new TransactionModel
            {
                SiteName = string.Empty,
                BetCount = a.BetCount + b.BetCount,
                Turnover = a.Turnover + b.Turnover,
                MemberWinLoss = a.MemberWinLoss + b.MemberWinLoss,
                AgentWinLoss = a.AgentWinLoss + b.AgentWinLoss,
                MasterWinLoss = a.MasterWinLoss + b.MasterWinLoss,
                SuperWinLoss = a.SuperWinLoss + b.SuperWinLoss,
                CompanyWinLoss = a.CompanyWinLoss + b.CompanyWinLoss,
                TotalWinLoss = a.TotalWinLoss + b.TotalWinLoss,
                Commission = a.Commission + b.Commission,
                ExtraPT = a.ExtraPT + b.ExtraPT
            };
        }

        public List<TransactionModel> Items { get; set; }

        public TransactionModel Total { get; set; }
    }
}
