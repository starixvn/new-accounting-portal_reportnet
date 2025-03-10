namespace BetConstructPerformance.Models;

using Epplus.Mapper.Annotations;

public class SportDetailItemExcelModel
{
    [Cell("A4")]
    public DateTime Date { get; set; }

    [Cell("B4")]
    public string Sport { get; set; }

    [Cell("C4")]
    public string LeagueName { get; set; }

    [Cell("D4")]
    public string MatchName { get; set; }

    [Cell("E4")]
    public string BetTypeName { get; set; }

    [Cell("F4")]
    public string LiveStatus { get; set; }

    [Cell("G4")]
    public int MatchCount { get; set; }

    [Cell("H4")]
    public int BetCount { get; set; }

    [Cell("I4")]
    public decimal MemberWinLoss { get; set; }

    [Cell("J4")]
    public decimal MemberTurnover { get; set; }

    [Cell("K4")]
    public decimal HouseIncome { get; set; }

    [Cell("L4")]
    public decimal HouseTurnover { get; set; }

    [Cell("M4")]
    public decimal Margin { get; set; }

    [Cell("N4")]
    public decimal AmountDueToBC { get; set; }
}