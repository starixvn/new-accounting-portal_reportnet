namespace BetCount.Model
{
    public class TransactionModel
    {
        public string SiteName { get; set; }

        public int BetCount { get; set; }

        public decimal Turnover { get; set; }

        public decimal MemberWinLoss { get; set; }

        public decimal MarginPercent { get; set; }

        public decimal AgentWinLoss { get; set; }

        public decimal MasterWinLoss { get; set; }

        public decimal SuperWinLoss { get; set; }

        public decimal CompanyWinLoss { get; set; }

        public decimal TotalWinLoss { get; set; }

        public decimal Commission { get; set; }

        public decimal ExtraPT { get; set; }

        public List<TransactionModel> Items { get; set; }

        public TransactionModel Total { get; set; }
    }
}
