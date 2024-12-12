namespace TreeWinLoss.Model
{
    public class TreeWinLossVirtualRateModel
    {
        public int NumberUserEnabledVirtualRate { get; set; }

        public Dictionary<string, int> EnabledVirtualRateUser { get; set; }

        public SMAVirtualRateModel SMAVirtualRate { get; set; } = new();
    }
}
