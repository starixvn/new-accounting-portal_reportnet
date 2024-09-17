using Epplus.Mapper.Annotations;

namespace DailyCreatedCustomers.Model;

public class SummaryItemExcelModel
{
    [Cell("A5")]
    public string CreatedDate { get; set; }

    [Cell("B5")]
    public int SuperAmount { get; set; }

    [Cell("C5")]
    public int DirectMasterAmount { get; set; }

    [Cell("D5")]
    public int DirectAgentAmount { get; set; }

    [Cell("E5")]
    public int MasterAmount { get; set; }

    [Cell("F5")]
    public int AgentAmount { get; set; }

    [Cell("G5")]
    public int MemberAmount { get; set; }
}