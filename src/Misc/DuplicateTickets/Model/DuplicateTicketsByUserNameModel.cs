namespace DuplicateTickets.Model;

public class DuplicateTicketsByUserNameModel
{
    public string UserName { get; set; }

    public int CustId { get; set; }

    public string BetType { get; set; }

    public int BetTypeId { get; set; }

    public string WinLostDate { get; set; }

    public decimal BalanceBeforeBet { get; set; }

    public decimal Stake { get; set; }

    public decimal Odds { get; set; }

    public List<TicketModel> TicketInfo { get; set; } = [];
}