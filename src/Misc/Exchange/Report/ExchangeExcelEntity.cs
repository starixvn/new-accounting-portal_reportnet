namespace Exchange.Report;

using Epplus.Mapper.Annotations;

public class ExchangeExcelEntity
{
    [Cell("A5")]
    public string Name { get; set; }

    [Cell("B5")]
    public int MemberCount { get; set; }

    [Cell("C5")]
    public int BetCount { get; set; }

    [Cell("D5")]
    public decimal TurnOver { get; set; }

    [Cell("E5")]
    public decimal MemberWinLoss { get; set; }

    [Cell("F5")]
    public decimal MemberCommission { get; set; }

    [Cell("G5")]
    public decimal MemberTotal { get; set; }

    [Cell("H5")]
    public decimal LicenseeWinLoss { get; set; }

    [Cell("I5")]
    public decimal LicenseeCommission { get; set; }

    [Cell("J5")]
    public decimal LicenseeTotal { get; set; }

    [Cell("K5")]
    public decimal CompanyWinLoss { get; set; }

    [Cell("L5")]
    public decimal CompanyCommission { get; set; }

    [Cell("M5")]
    public decimal PayToSupplier { get; set; }

    [Cell("N5")]
    public decimal CompanyNetIncome { get; set; }

    public string MarketType { get; set; }
}