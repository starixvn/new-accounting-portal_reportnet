namespace SoccerPerformanceDetail.ViewModel;

using Model;

public class MatchPerformanceGroup()
{
    public MatchPerformanceGroup(int? leagueId)
        : this()
    {
        if (leagueId.HasValue)
        {
            LeagueId = leagueId.Value;
            LeagueName = string.Empty;//CacheRef.GetLeagueName(this.LeagueId);
        }
    }

    public int LeagueId { get; set; }

    public string LeagueName { get; set; }

    public ICollection<MatchPerformanceItem> ItemLiveList { get; set; } = new LinkedList<MatchPerformanceItem>();

    public ICollection<MatchPerformanceItem> ItemNonLiveList { get; set; } = new LinkedList<MatchPerformanceItem>();

    public MatchPerformanceItem Total { get; set; } = new();

    public void AddMatchList(IEnumerable<MatchPerformanceInfo> matchPerformanceInfos, bool isLive)
    {
        foreach (var item in matchPerformanceInfos)
        {
            if (item != null)
            {
                AddItem(new MatchPerformanceItem(item), isLive);
            }
        }
    }

    public void AddMatchList(IEnumerable<MatchPerformanceRawInfo> matchPerformanceInfos, bool isLive)
    {
        foreach (var item in matchPerformanceInfos)
        {
            if (item != null)
            {
                AddItem(new MatchPerformanceItem(item), isLive);
            }
        }
    }

    public void AddMatchList(IGrouping<int?, MatchPerformanceInfo> grouping, bool isLive)
    {
        foreach (var item in grouping)
        {
            if (item != null)
            {
                AddItem(new MatchPerformanceItem(item), isLive);
            }
        }
    }

    private void AddItem(MatchPerformanceItem item, bool isLive)
    {
        if (isLive)
        {
            ItemLiveList.Add(item);
        }
        else
        {
            ItemNonLiveList.Add(item);
        }

        CalculateTotal(item);
    }

    private void CalculateTotal(MatchPerformanceItem item)
    {
        Total.Turnover += item.Turnover;
        Total.WinLoss += item.WinLoss;
        Total.TurnoverReal += item.TurnoverReal;
        Total.WinLossReal += item.WinLossReal;
        Total.BetCount += item.BetCount;
        Total.WinLossScore += item.WinLossScore;
        Total.SumCategory += item.SumCategory;
    }
}