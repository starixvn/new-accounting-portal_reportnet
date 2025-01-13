namespace SMAWinLoss.Summary;

public class UsernameWinLoss
{
    public int CustId { get; set; }

    public string Username { get; set; }

    public string Role { get; set; }

    public string Currency { get; set; }

    public string Name { get; set; }

    public string VirtualRate { get; set; }

    public List<MonthModel> Months { get; set; }
}