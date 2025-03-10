namespace BetCount.Model;

using Epplus.Mapper.Annotations;

public class TransactionExcelModel
{
    [Cell("A6")]
    public string SiteName { get; set; }

    [Cell("B6")]
    public int BetCount { get; set; }

    [Cell("C6")]
    public decimal Turnover { get; set; }

    [Cell("D6")]
    public decimal MemberWinLoss { get; set; }

    [Cell("E6")]
    public decimal MarginPercent { get; set; }

    [Cell("F6")]
    public decimal AgentWinLoss { get; set; }

    [Cell("G6")]
    public decimal MasterWinLoss { get; set; }

    [Cell("H6")]
    public decimal SuperWinLoss { get; set; }

    [Cell("I6")]
    public decimal TotalWinLoss { get; set; }

    [Cell("J6")]
    public decimal Commission { get; set; }

    [Cell("K6")]
    public decimal ExtraPT { get; set; }

    [Cell("L6")]
    public decimal CompanyWinLoss { get; set; }

    public List<TransactionExcelModel> Items { get; set; }

    public TransactionExcelModel Total { get; set; }
}