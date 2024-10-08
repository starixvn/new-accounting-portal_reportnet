namespace ParlayWon.Entities
{
    public class ParlayWonEntity
    {
        public ulong TransId { get; set; }

        public string Username { get; set; }

        public string Currency {  get; set; }

        public decimal Exchange {  get; set; }  

        public decimal Stake { get; set; }  

        public decimal ActualStake { get; set; }

        public decimal WinLoss { get; set; }

        public decimal StakeRM { get; set; }

        public decimal ActualStakeRM { get; set; }

        public decimal WinLossRM { get; set; }

        public decimal CompanyStakeRM { get; set; }

        public decimal CompanyActualStakeRM { get; set; }

        public decimal CompanyWinLossRM { get; set; }

        public int Selection {  get; set; }

        public decimal Odds { get; set; }   

        public decimal MOdds { get; set;}
    }
}
