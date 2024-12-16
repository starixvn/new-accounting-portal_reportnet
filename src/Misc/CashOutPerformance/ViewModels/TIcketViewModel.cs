namespace CashOutPerformance.ViewModels;

public class TicketViewModel<T>
{
    public IEnumerable<T> TicketList { get; set; }

    public T TotalTicket { get; set; }

    public int Total { get; set; }
}