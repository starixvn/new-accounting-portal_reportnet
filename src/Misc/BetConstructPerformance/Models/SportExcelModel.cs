namespace BetConstructPerformance.Models;

public class SportExcelModel
{
    public IEnumerable<SportItemExcelModel> Items { get; set; }

    public SportItemExcelModel Total { get; set; }
}