namespace DailyMixParlay.Models
{
    public class WinLossByCombination
    {
        public string Name { get; set; }

        public List<WinLossByLiveStatus> WinLosses { get; set; }

        public WinLossItem Total { get; set; }
    }
}
