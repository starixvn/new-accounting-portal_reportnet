namespace DailyMixParlay.Models
{
    public class WinLoss
    {
        public string Name { get; set; }

        public List<WinLossItem> WinLosses { get; set; }

        public WinLossItem GrandTotal { get; set; }
    }
}
