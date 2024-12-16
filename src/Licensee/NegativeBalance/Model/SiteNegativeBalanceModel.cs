namespace NegativeBalance.Model;

public class SiteNegativeBalanceModel
{
    public string LicenseeSite { get; set; }

    public int TotalNegativeBalance { get; set; }

    public List<NegativeBalanceUser> NegativeBalanceUsers{ get; set; } = [];
}