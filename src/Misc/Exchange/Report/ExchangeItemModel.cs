namespace Exchange.Report;

public class ExchangeItemModel
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

    public static ExchangeItemModel operator +(ExchangeItemModel a, ExchangeItemModel b)
    {
        return new ExchangeItemModel
        {
            Name = a.Name,
            MarketType = a.MarketType,
            MemberCount = a.MemberCount + b.MemberCount,
            BetCount = a.BetCount + b.BetCount,
            TurnOver = a.TurnOver + b.TurnOver,
            MemberWinLoss = a.MemberWinLoss + b.MemberWinLoss,
            MemberCommission = a.MemberCommission + b.MemberCommission,
            MemberTotal = a.MemberTotal + b.MemberTotal,
            LicenseeWinLoss = a.LicenseeWinLoss + b.LicenseeWinLoss,
            LicenseeCommission = a.LicenseeCommission + b.LicenseeCommission,
            LicenseeTotal = a.LicenseeTotal + b.LicenseeTotal,
            CompanyWinLoss = a.CompanyWinLoss + b.CompanyWinLoss,
            CompanyCommission = a.CompanyCommission + b.CompanyCommission,
            PayToSupplier = a.PayToSupplier + b.PayToSupplier,
            CompanyNetIncome = a.CompanyNetIncome + b.CompanyNetIncome
        };
    }
}