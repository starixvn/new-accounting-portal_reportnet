using Epplus.Mapper.Annotations;

namespace LeagueWinLoss.Model;

public class WinLossItemExcelModel
{

    [Cell("B6")]
    public decimal MemberWinLoss { get; set; }

    [Cell("C6")]
    public decimal SMAWinLoss { get; set; }

    [Cell("D6")]
    public decimal ExtraPT { get; set; }

    [Cell("E6")]
    public decimal Commission { get; set; }

    [Cell("F6")]
    public decimal VarianceIncome { get; set; }

    [Cell("G6")]
    public decimal VirtualWinLoss { get; set; }

    [Cell("H6")]
    public decimal RealIncome { get; set; }

    [Cell("I6")]
    public decimal VirtualTurnover { get; set; }

    [Cell("J6")]
    public decimal RealTurnover { get; set; }

    [Cell("K6")]
    public decimal VirtualMargin { get; set; }

    [Cell("L6")]
    public decimal RealMargin { get; set; }
}

public class MatchWinLossItemExcelModel : WinLossItemExcelModel
{
    [Cell("A6")]
    public string MatchEvent { get; set; }
}

public class BetTypeWinLossItemExcelModel : WinLossItemExcelModel
{
    [Cell("A6")]
    public string BettypeName { get; set; }
}

public class SiteWinLossItemExcelModel : WinLossItemExcelModel
{
    [Cell("A6")]
    public string SiteName { get; set; }
}

public class SuperWinLossItemExcelModel : WinLossItemExcelModel
{
    [Cell("A6")]
    public string SuperName { get; set; }
}