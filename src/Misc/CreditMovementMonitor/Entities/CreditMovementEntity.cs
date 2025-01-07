namespace CreditMovementMonitor.Entities;

public class CreditMovementEntity
{
    public string SuperName { get; set; }

    public string MasterName { get; set; }

    public string AgentName { get; set; }

    public string CustomerName { get; set; }

    public DateTime TransDate { get; set; }

    public string Currency { get; set; }

    public string Site { get; set; }

    public decimal Amount { get; set; }

    public decimal BaseAmount { get; set; }
}