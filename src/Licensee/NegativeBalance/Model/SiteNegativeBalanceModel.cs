namespace NegativeBalance.Model
{
    public class SiteNegativeBalanceModel
    {
        public SiteNegativeBalanceModel()
        {
            NegativeBalanceUsers = new List<NegativeBalanceUser>();
        }

        public string LicenseeSite { get; set; }

        public int TotalNegativeBalance { get; set; }

        public List<NegativeBalanceUser> NegativeBalanceUsers{ get; set; }
    }
}
