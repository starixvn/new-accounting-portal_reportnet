namespace DailyCreatedCustomers.Model;

public class SummaryItemModel
{
    public string Name { get; set; }

    public string CreatedDate { get; set; }

    public int SuperAmount { get; set; }

    public int MasterAmount { get; set; }

    public int AgentAmount { get; set; }

    public int MemberAmount { get; set; }

    public int DirectMasterAmount { get; set; }

    public int DirectAgentAmount { get; set; }

    public static SummaryItemModel operator +(SummaryItemModel a, SummaryItemModel b)
    {
        return new SummaryItemModel
        {
            Name = string.Empty,
            CreatedDate = string.Empty,
            SuperAmount = a.SuperAmount + b.SuperAmount,
            MasterAmount = a.MasterAmount + b.MasterAmount,
            AgentAmount = a.AgentAmount + b.AgentAmount,
            DirectMasterAmount = a.DirectMasterAmount + b.DirectMasterAmount,
            DirectAgentAmount = a.DirectAgentAmount + b.DirectAgentAmount,
            MemberAmount = a.MemberAmount + b.MemberAmount
        };
    }
}