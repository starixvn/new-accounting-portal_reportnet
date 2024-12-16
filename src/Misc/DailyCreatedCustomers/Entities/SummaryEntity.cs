namespace DailyCreatedCustomers.Entities;

public class SummaryEntity
{
    public DateTime CreatedDate { get; set; }

    public int SuperAmount { get; set; }

    public int MasterAmount { get; set; }

    public int AgentAmount { get; set; }

    public int MemberAmount { get; set; }

    public int DirectMasterAmount { get; set; }

    public int DirectAgentAmount { get; set; }
}