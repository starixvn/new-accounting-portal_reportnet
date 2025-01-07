namespace CompanyBalanceDifference.Entities;

public class CompanyBalanceDifferenceItem
{
    public DateTime BalanceUpDay { get; set; }

    public decimal CompanyBalanceBefore { get; set; }

    public decimal CompanyBalanceAfter { get; set; }

    public decimal Difference { get; set; }
}