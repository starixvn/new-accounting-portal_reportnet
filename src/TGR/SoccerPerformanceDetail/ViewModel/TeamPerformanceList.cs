namespace SoccerPerformanceDetail.ViewModel;
public class TeamPerformanceList
{
    public List<TeamPerformanceItem> ItemLiveList { get; set; } = [];

    public List<TeamPerformanceItem> ItemNonLiveList { get; set; } = [];

    public TeamPerformanceItem Total { get; set; } = new();
    
}