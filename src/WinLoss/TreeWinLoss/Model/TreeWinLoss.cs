namespace TreeWinLoss.Model
{
    public class TreeWinLoss
    {
        public List<TreeWinLossInfo> ChildsInfo { get; set; }

        public List<TreeWinLossInfo> TotalInfo { get; set; }

        public bool IsShowNotAvailableTurnover { get; set; }
    }
}
