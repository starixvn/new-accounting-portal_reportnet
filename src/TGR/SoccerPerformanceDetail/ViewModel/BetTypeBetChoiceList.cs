namespace SoccerPerformanceDetail.ViewModel;

using Model;

public class BetTypeBetChoiceList
{
    public bool IsFMBetType { get; set; } = false;

    public bool IsBetChoiceDetail { get; set; } = false;

    public List<BetTypeBetChoiceItem> ItemList { get; set; } = [];


    public BetTypeBetChoiceItem Total { get; set; } = new();

    public void AddItem(IEnumerable<BetTypeBetChoicePerformanceInfo> infos)
    {
        foreach (var info in infos)
        {
            if (info != null)
            {
                var item = new BetTypeBetChoiceItem(info);

                ItemList.Add(item);

                CalculateTotal(item);
            }
        }

        Total.Margin = CalculateTotalMargin(Total.WinLoss, Total.Turnover);
        Total.MarginReal = CalculateTotalMargin(Total.WinLossReal, Total.TurnoverReal);
    }

    private void CalculateTotal(BetTypeBetChoiceItem item)
    {           
        Total.Turnover += item.Turnover;
        Total.WinLoss += item.WinLoss;
        Total.TurnoverReal += item.TurnoverReal;
        Total.WinLossReal += item.WinLossReal;
        Total.MatchCount += item.MatchCount;
        Total.BetCount += item.BetCount;
    }

    private decimal? CalculateTotalMargin(decimal winLoss, decimal turnover)
    {
        decimal? margin = null;

        if (turnover != 0)
        {
            margin = winLoss / turnover * 100;
        }

        return margin;
    }

}
