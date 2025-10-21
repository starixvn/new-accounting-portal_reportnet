namespace DstPerformance.Models;

public class SportExcelModel
{
    public IEnumerable<SportItemExcelModel> Items { get; set; }

    public SportItemExcelModel Total { get; set; }
}