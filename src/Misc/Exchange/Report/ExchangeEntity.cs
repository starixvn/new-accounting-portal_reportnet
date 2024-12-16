namespace Exchange.Report;

public class ExchangeEntity
{
    public string Name { get; set; }

    public string MarketType { get; set; }

    public int MemberCount { get; set; }

    public int BetCount { get; set; }

    public decimal TurnOver { get; set; }

    public decimal MemberWinLoss { get; set; }

    public decimal MemberCommission { get; set; }

    public decimal MemberTotal { get; set; }

    public decimal LicenseeWinLoss { get; set; }

    public decimal LicenseeCommission { get; set; }

    public decimal LicenseeTotal { get; set; }

    public decimal CompanyWinLoss { get; set; }

    public decimal CompanyCommission { get; set; }

    public decimal PayToSupplier { get; set; }

    public decimal CompanyNetIncome { get; set; }
}