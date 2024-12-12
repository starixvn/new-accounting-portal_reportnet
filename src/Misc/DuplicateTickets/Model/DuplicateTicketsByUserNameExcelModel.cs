using Epplus.Mapper.Annotations;

namespace DuplicateTickets.Model
{
    public class DuplicateTicketsByUserNameExcelModel
    {
        [Cell("A4")]
        public int No { get; set; }

        [Cell("B4")]
        public string UserName { get; set; }

        [Cell("C4")]
        public int CustId { get; set; }

        [Cell("D4")]
        public string BetType { get; set; }

        [Cell("E4")]
        public string WinLostDate { get; set; }


        [Cell("F4")]
        public decimal BalanceBeforeBet { get; set; }

        [Cell("G4")]
        public decimal Stake { get; set; }

        [Cell("H4")]
        public decimal Odds { get; set; }

        public List<TicketExcelModel> TicketInfo { get; set; } = [];
    }
}
