using TreeWinLoss.Model;

namespace WTreeWinLoss.Model
{
    public class TreeWinLossVirtualRate
    {
        public TreeWinLossVirtualRate()
        {
            SMAVirtualRate = new SMAVirtualRate();
        }

        public int NumberUserEnabledVirtualRate { get; set; }

        public Dictionary<string, int> EnabledVirtualRateUser { get; set; }

        public SMAVirtualRate SMAVirtualRate { get; set; }
    }
}
