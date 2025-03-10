namespace SoccerPerformanceDetail.ViewModel;

public class MatchPerformanceList
{
    public int TotalItems { get; set; }

    public ICollection<MatchPerformanceGroup> GroupList { get; set; } = new LinkedList<MatchPerformanceGroup>();

    public MatchPerformanceItem GrandTotal { get; set; } = new();

    public void AddItem(MatchPerformanceGroup group)
    {
        if (group != null)
        {
            GroupList.Add(group);
            GrandTotal.Turnover += group.Total.Turnover;
            GrandTotal.WinLoss += group.Total.WinLoss;
            GrandTotal.TurnoverReal += group.Total.TurnoverReal;
            GrandTotal.WinLossReal += group.Total.WinLossReal;
            GrandTotal.BetCount += group.Total.BetCount;
        }
    }
}