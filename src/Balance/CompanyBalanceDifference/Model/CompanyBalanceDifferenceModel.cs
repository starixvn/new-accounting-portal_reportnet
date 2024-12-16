namespace CompanyBalanceDifference.Model;

public class CompanyBalanceDifferenceModel
{
    public string BalanceUpDate { get; set; }

    public decimal BalanceBeforeBalanceUpDate { get; set; }

    public decimal BalanceAfterBalanceUpDate { get; set; }

    public decimal Difference { get; set; }
}