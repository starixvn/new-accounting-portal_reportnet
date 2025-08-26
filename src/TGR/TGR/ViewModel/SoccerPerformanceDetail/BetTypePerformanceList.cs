namespace TGR.ViewModel.SoccerPerformanceDetail;

public class BetTypePerformanceList
{
    public List<BetTypePerformanceItem> ItemByOrderList { get; set; } = [];

    public List<BetTypePerformanceItem> ItemLiveList { get; set; } = [];

    public List<BetTypePerformanceItem> ItemNonLiveList { get; set; } = [];

    public BetTypePerformanceItem Total { get; set; } = new();

    public BetTypePerformanceItem TotalItemLive { get; set; } = new();

    public BetTypePerformanceItem TotalItemNonLive { get; set; } = new();
}