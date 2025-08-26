namespace TGR.ViewModel.SoccerPerformanceDetail;

public class LeaguePerformanceList
{
    public List<LeaguePerformanceItem> ItemByOrderList { get; set; } = [];

    public List<LeaguePerformanceItem> ItemLiveList { get; set; } = [];

    public List<LeaguePerformanceItem> ItemNonLiveList { get; set; } = [];

    public LeaguePerformanceItem Total { get; set; } = new();

    public LeaguePerformanceItem TotalItemLive { get; set; } = new();

    public LeaguePerformanceItem TotalItemNonLive { get; set; } = new();
}