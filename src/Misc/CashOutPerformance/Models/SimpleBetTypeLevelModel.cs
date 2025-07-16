namespace CashOutPerformance.Models;

public class SimpleBetTypeLevelModel : CommonTicketModel
{
    public int BetType { get; set; }
    public string BetTypeName { get; set; }
    public int BetId { get; set; }
}