namespace TGR.ViewModel;

public class GradePerformanceList
{
    public List<GradePerformanceItem> ItemLiveList { get; set; } = [];

    public List<GradePerformanceItem> ItemNonLiveList { get; set; } = [];

    public GradePerformanceItem Total { get; set; } = new();
}