namespace SoccerPerformanceDetail.ViewModel
{
    public class MatchPerformanceList
    {
        public MatchPerformanceList()
        {
            this.GroupList = new LinkedList<MatchPerformanceGroup>();
            this.GrandTotal = new MatchPerformanceItem();
        }

        public int TotalItems { get; set; }

        public ICollection<MatchPerformanceGroup> GroupList { get; set; }

        public MatchPerformanceItem GrandTotal { get; set; }

        public void AddItem(MatchPerformanceGroup group)
        {
            if (group != null)
            {
                this.GroupList.Add(group);
                this.GrandTotal.Turnover += group.Total.Turnover;
                this.GrandTotal.WinLoss += group.Total.WinLoss;
                this.GrandTotal.TurnoverReal += group.Total.TurnoverReal;
                this.GrandTotal.WinLossReal += group.Total.WinLossReal;
                this.GrandTotal.BetCount += group.Total.BetCount;
            }
        }
    }
}