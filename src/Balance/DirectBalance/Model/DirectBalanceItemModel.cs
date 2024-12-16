namespace DirectBalance.Model;

public class DirectBalanceItemModel
{
    public string CustId { get; set; }

    public string Username { get; set; }

    public string Currency { get; set; }

    public string CurrencyName { get;set; }

    public decimal Balance { get; set; }

    public decimal BaseBalance { get; set; }

    public static DirectBalanceItemModel operator +(DirectBalanceItemModel a, DirectBalanceItemModel b)
    {
        return new DirectBalanceItemModel
        {
            CustId = string.Empty,
            Username = string.Empty,
            Currency = string.Empty,
            Balance = a.Balance + b.Balance,
            BaseBalance = a.BaseBalance + b.BaseBalance 
        };
    }
}