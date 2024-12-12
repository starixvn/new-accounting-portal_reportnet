namespace NegativeBalance.Model
{ 
    public class NegativeBalanceModel
    {
        public List<SiteNegativeBalanceModel> NegativeBalanceUsers { get; set; } = [];

        public int TotalNegativeBalanceUser { get; set; }
    }
}
