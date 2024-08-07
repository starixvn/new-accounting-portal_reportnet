namespace TreeWinLoss.Model
{
    public class TreeWinLossVirtualRateModel
    {
        public TreeWinLossVirtualRateModel()
        {
            SMAVirtualRate = new SMAVirtualRateModel();
        }

        public int NumberUserEnabledVirtualRate { get; set; }

        public Dictionary<string, int> EnabledVirtualRateUser { get; set; }

        public SMAVirtualRateModel SMAVirtualRate { get; set; }
    }
}
