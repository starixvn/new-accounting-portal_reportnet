namespace DailyMixParlay.Models
{
    public class ChildGroupItem
    {
        public string Name { get; set; }

        public List<WinLossItem> WinLosses { get; set; }

        public WinLossItem Total { get; set; }
    }
}
