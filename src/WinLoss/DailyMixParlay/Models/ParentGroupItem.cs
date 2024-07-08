namespace DailyMixParlay.Models
{
    public class ParentGroupItem
    {
        public string Name { get; set; }

        public List<ChildGroupItem> WinLosses { get; set; }

        public WinLossItem Total { get; set; }

        public int MergeRowNumber { get; set; }
    }
}
