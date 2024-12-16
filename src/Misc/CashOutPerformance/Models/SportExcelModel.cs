namespace CashOutPerformance.Models;

public class SportExcelModel
{
    public SportItemExcelModel SportTotal { get; set; }

    public IEnumerable<SportItemExcelModel> ChildItems { get; set; }
}