namespace SoccerPerformanceDetail.ViewModel;

public class BetTypeBetChoiceList
{
    public bool IsFMBetType { get; set; } = false;

    public bool IsBetChoiceDetail { get; set; } = false;

    public List<BetTypeBetChoiceItem> ItemList { get; set; } = [];

    public BetTypeBetChoiceItem Total { get; set; } = new();
}
