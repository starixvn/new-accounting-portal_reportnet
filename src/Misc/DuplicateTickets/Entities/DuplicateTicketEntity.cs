namespace DuplicateTickets.Entities
{
    public class DuplicateTicketEntity
    {
        public string UserName { get; set; }

        public int CustId { get; set; }

        public string TypeName { get; set; }

        public DateTime WinLostDate { get; set; }

        public decimal Balance { get; set; }

        public decimal Stake { get; set; }

        public decimal Odds { get; set; }

        public int Bettype { get; set; }

        public string RefNo { get; set; }

        public int RefNoGroup { get; set; }

        public int MatchId { get; set; }

        public string Ip { get; set; }

        public bool WalletType { get; set; }

        public string BetTeam { get; set; }

        public int OddsId { get; set; }

        public DateTime BetTime { get; set; }

        public string Status { get; set; }  

        public int StatusId { get; set; }

        public string TransDesc { get; set; }

        public string BetFrom { get; set; }

        public string SiteName { get; set; }
    }
}
