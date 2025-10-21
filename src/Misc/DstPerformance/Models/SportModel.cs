namespace DstPerformance.Models;

public class SportModel
{
    public IEnumerable<SportItemModel> Items { get; set; }

    public SportItemModel Total { get; set; }
}