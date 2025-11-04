using DstPerformance.Constants;
using DstPerformance.Services;
using Epplus.Mapper.Annotations;

namespace DstPerformance.Models;

public class ReportItemExcelModel
{
    public string Level { get; set; }

    public string Sport { get; set; }

    public int SportId { get; set; }

    public string LeagueName { get; set; }

    public string MatchName { get; set; }

    public string BetTypeName { get; set; }

    public int MatchCount { get; set; }

    [Cell("A4")]
    public DateTime Date { get; set; }

    [Cell("B4")]
    public string LevelText => Level switch
    {
        LevelConstants.Sport => Sport,
        LevelConstants.BetType => BetTypeName,
        LevelConstants.League => LeagueName,
        _ => MatchName
    };

    [Cell("C4")] public string MatchCountString => SportId.IsParlay() ? "-" : $"{MatchCount:#,##0}";

    [Cell("D4")]
    public int BetCount { get; set; }

    [Cell("E4")]
    public decimal MemberWinLoss { get; set; }

    [Cell("F4")]
    public decimal MemberTurnover { get; set; }

    [Cell("G4")]
    public decimal ActualTurnover { get; set; }

    [Cell("H4")]
    public decimal HouseIncome { get; set; }

    [Cell("I4")]
    public decimal HouseTurnover { get; set; }

    [Cell("J4")]
    public decimal Margin { get; set; }

    [Cell("K4")]
    public decimal AmountDueToDST { get; set; }
}