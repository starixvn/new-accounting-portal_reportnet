namespace DstPerformance.Models;

public class ReportModel
{
    public IEnumerable<ReportItemModel> Items { get; set; }

    public ReportItemModel Total { get; set; }
}