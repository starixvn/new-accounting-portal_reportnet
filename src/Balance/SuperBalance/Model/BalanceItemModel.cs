namespace SuperBalance.Model;

public class BalanceItemModel
{
    public string CustId { get; set; }

    public string Username { get; set; }

    public string Role { get; set; }

    public string Currency { get; set; }

    public string CurrencyName { get; set; }

    public bool HeadlineEnabled { get; set; }

    public decimal Balance { get; set; }

    public decimal BaseBalance { get; set; }

    public decimal Reward { get; set; }

    public decimal RewardRM { get; set; }

    public static BalanceItemModel operator +(BalanceItemModel a, BalanceItemModel b)
    {
        return new BalanceItemModel
        {
            BaseBalance = a.BaseBalance + b.BaseBalance,
            RewardRM = a.RewardRM + b.RewardRM
        };
    }
}