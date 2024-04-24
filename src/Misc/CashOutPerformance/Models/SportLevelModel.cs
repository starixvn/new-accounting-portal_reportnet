namespace CashOutPerformance.Models
{
    public class SportLevelModel
    {
        public IEnumerable<SportModel> Sports { get; set; }

        public SportItemModel GrandTotal { get; set; }

        public int TotalItems { get; set; }
    }
}