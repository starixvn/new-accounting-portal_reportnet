namespace ParlayWon.Model;

using Epplus.Mapper.Annotations;

public class ParlayWonTransactionExcelModel
{
    [Cell("A6")]
    public ulong RefNo { get; set; }

    [Cell("B6")]
    public string UserName { get; set; }

    [Cell("C6")]
    public string Currency { get; set; }

    [Cell("D6")]
    public decimal Exchange { get; set; }

    [Cell("E6")]
    public decimal OriginalStake { get; set; }

    [Cell("F6")]
    public decimal OriginalWinLoss { get; set; }

    [Cell("G6")]
    public decimal BaseStake { get; set; }

    [Cell("H6")]
    public decimal BaseWinLoss { get; set; }

    [Cell("I6")]
    public decimal CompanyStake { get; set; }

    [Cell("J6")]
    public decimal CompanyWinLoss { get; set; }

    [Cell("K6")]
    public int Selection { get; set; }

    [Cell("L6")]
    public decimal WinOdds { get; set; }

    public List<ParlayWonTransactionExcelModel> Items { get; set; }

    public ParlayWonTransactionExcelModel Total { get; set; }
}