namespace SoccerPerformanceDetail.ViewModel;

using Model;

public class TraderPerformanceList
{
    public List<TraderPerformanceItem> ItemLiveList { get; set; } = [];

    public List<TraderPerformanceItem> ItemNonLiveList { get; set; } = [];

    public TraderPerformanceItem Total { get; set; } = new();

    //public void AddTraderList(IEnumerable<TraderPerformanceInfo> traderPerformanceInfos, bool isLive)
    //{
    //    foreach (var item in traderPerformanceInfos)
    //    {
    //        if (item != null)
    //        {
    //            this.AddItem(new TraderPerformanceItem(item), isLive);
    //        }                
    //    }
    //}

    //private void AddItem(TraderPerformanceItem item, bool isLive)
    //{
    //    if (isLive)
    //    {
    //        this.ItemLiveList.Add(item);
    //    }
    //    else
    //    {
    //        this.ItemNonLiveList.Add(item);
    //    }

    //    CalculateTotal(item);
    //}

    //private void CalculateTotal(TraderPerformanceItem item)
    //{
    //    this.Total.NoOfLeagues += item.NoOfLeagues;
    //    this.Total.NoOfMatches += item.NoOfMatches;
    //    this.Total.Turnover += item.Turnover;
    //    this.Total.WinLoss += item.WinLoss;           
    //    this.Total.TurnoverReal += item.TurnoverReal;
    //    this.Total.WinLossReal += item.WinLossReal;
    //    this.Total.BetCount += item.BetCount;
    //}
}