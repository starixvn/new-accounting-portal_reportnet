namespace DailyMixParlay.Models
{
    public class WinLoss
    {
        public string Name { get; set; }

        public List<WinLossByLiveStatus> WinLosses { get; set; }

        public WinLossItem Total { get; set; }
    }
}
