namespace CashOutPerformance.Models;

using Epplus.Mapper.Annotations;

public class SimpleBetTypeLevelExcelModel
{
    [Cell("A6")]
    public string BetTypeName { get; set; }

    [Cell("B6")]
    public decimal COTurnover_RM { get; set; }

    [Cell("C6")]
    public decimal? ActualTurnover { get; set; }

    [Cell("D6")]
    public decimal BBAmount_RM { get; set; }

    [Cell("E6")]
    public decimal MemberWL_RM { get; set; }

    [Cell("F6")]
    public decimal? AssumedPayout { get; set; }

    [Cell("G6")]
    public decimal? HypGGR { get; set; }

    [Cell("H6")]
    public decimal? COProfit { get; set; }

    [Cell("I6")]
    public decimal? Margin { get; set; }

    [Cell("J6")]
    public decimal Comm_RM { get; set; }

    [Cell("K6")]
    public decimal COIncome_RM { get; set; }

    [Cell("L6")]
    public decimal TotalIncome_RM { get; set; }
}