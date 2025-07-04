namespace TGR.ViewModel;

public class MatchPerformanceGroup
{
    public int LeagueId { get; set; }

    public string LeagueName { get; set; }

    public List<MatchPerformanceItem> ItemLiveList { get; set; } = [];

    public List<MatchPerformanceItem> ItemNonLiveList { get; set; } = [];

    public MatchPerformanceItem Total { get; set; } = new();
}