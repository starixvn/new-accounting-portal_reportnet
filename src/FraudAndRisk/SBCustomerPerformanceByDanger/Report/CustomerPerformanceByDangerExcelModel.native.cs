namespace SBCustomerPerformanceByDanger.Report;

using Epplus.Mapper.Annotations;

public class CustomerPerformanceByDangerExcelModel
{
    [Cell("A4")]
    public string UserName { get; set; }

    [Cell("B4")]
    public string AgentName { get; set; }

    [Cell("C4")]
    public string MasterName { get; set; }

    [Cell("D4")]
    public string SuperName { get; set; }

    [Cell("E4")]
    public string Division { get; set; }

    [Cell("F4")]
    public string Currency { get; set; }

    [Cell("G4")]
    public string DangerLevel { get; set; }

    [Cell("H4")]
    public decimal GivenCredit { get; set; }

    [Cell("I4")]
    public int BetCount { get; set; }

    [Cell("J4")]
    public decimal MemberTO { get; set; }

    [Cell("K4")]
    public decimal MemberWL { get; set; }

    [Cell("L4")]
    public decimal Margin { get; set; }

    [Cell("M4")]
    public decimal AVGStake { get; set; }

    [Cell("N4")]
    public decimal HouseTO { get; set; }

    [Cell("O4")]
    public decimal HouseIncome { get; set; }

    [Cell("P4")]
    public int NumberOfAssociatedAccount { get; set; }

}