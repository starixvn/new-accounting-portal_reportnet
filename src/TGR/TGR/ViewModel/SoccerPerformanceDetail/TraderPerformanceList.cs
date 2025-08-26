namespace TGR.ViewModel.SoccerPerformanceDetail;

public class TraderPerformanceList
{
    public List<TraderPerformanceItem> ItemLiveList { get; set; } = [];

    public List<TraderPerformanceItem> ItemNonLiveList { get; set; } = [];

    public TraderPerformanceItem Total { get; set; } = new();
}