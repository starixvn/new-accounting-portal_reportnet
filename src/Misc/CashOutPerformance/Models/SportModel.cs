namespace CashOutPerformance.Models;

public class SportModel
{
    public SportItemModel SportTotal { get; set; }

    public IEnumerable<SportItemModel> ChildItems { get; set; }
}