namespace NegativeBalance.Model
{ 
    public class NegativeBalanceModel
    {
        public NegativeBalanceModel()
        {
            NegativeBalanceUsers = new List<SiteNegativeBalanceModel>();
        }

        public List<SiteNegativeBalanceModel> NegativeBalanceUsers { get; set; }

        public int TotalNegativeBalanceUser { get; set; }
    }
}
