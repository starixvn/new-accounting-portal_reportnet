using System.Transactions;

namespace ParlayWon.Model
{
    public class ParlayWonTransaction
    {
        public ulong RefNo { get; set; }

        public string UserName { get; set; }

        public string Currency { get; set; }

        public decimal Exchange { get; set; }

        public decimal OriginalStake { get; set; }

        public decimal OriginalWinLoss { get; set; }

        public decimal BaseStake { get; set; }

        public decimal BaseWinLoss { get; set; }

        public decimal CompanyStake { get; set; }

        public decimal CompanyWinLoss { get; set; }

        public int Selection { get; set; }

        public decimal WinOdds { get; set; }

        public int NumberItems { get; set; }

        public List<ParlayWonTransaction> Items { get; set; }

        public ParlayWonTransaction Total { get; set; }

        public static ParlayWonTransaction operator +(ParlayWonTransaction a, ParlayWonTransaction b)
        {
            return new ParlayWonTransaction
            {
                UserName = "Total",
                OriginalStake = a.OriginalStake + b.OriginalStake,
                OriginalWinLoss = a.OriginalWinLoss + b.OriginalWinLoss,
                BaseStake = a.BaseStake + b.BaseStake,
                BaseWinLoss = a.BaseWinLoss + b.BaseWinLoss,
                CompanyStake = a.CompanyStake + b.CompanyStake,
                CompanyWinLoss = a.CompanyWinLoss + b.CompanyWinLoss
            };
        }
    }
}
