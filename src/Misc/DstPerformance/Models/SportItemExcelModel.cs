using Epplus.Mapper.Annotations;

namespace DstPerformance.Models;

public class SportItemExcelModel
{
    [Cell("A4")]
    public string Sport { get; set; }

    [Cell("B4")]
    public int MatchCount { get; set; }

    [Cell("C4")]
    public int BetCount { get; set; }

    [Cell("D4")]
    public decimal MemberWinLoss { get; set; }

    [Cell("E4")]
    public decimal MemberTurnover { get; set; }

    [Cell("F4")]
    public decimal ActualTurnover { get; set; }

    [Cell("G4")]
    public decimal HouseIncome { get; set; }

    [Cell("H4")]
    public decimal HouseTurnover { get; set; }

    [Cell("I4")]
    public decimal Margin { get; set; }

    [Cell("J4")]
    public decimal AmountDueToDST { get; set; }
}