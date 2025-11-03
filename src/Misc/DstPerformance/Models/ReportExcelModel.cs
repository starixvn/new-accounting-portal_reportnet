namespace DstPerformance.Models;

public class ReportExcelModel
{
    public IEnumerable<ReportItemExcelModel> Items { get; set; }

    public ReportItemExcelModel Total { get; set; }
}