namespace CashOutPerformance.Models;

public class FilterGroupItemModel
{
    public int GroupId { get; set; }

    public string GroupName { get; set; }

    public List<FilterItemModel> ChildItems { get; set; }
}