namespace SoccerPerformanceDetail.ViewModel;

using Model;

public class BetTypePerformanceList
{
    public List<BetTypePerformanceItem> ItemByOrderList { get; set; } = [];

    public List<BetTypePerformanceItem> ItemLiveList { get; set; } = [];

    public List<BetTypePerformanceItem> ItemNonLiveList { get; set; } = [];

    public BetTypePerformanceItem Total { get; set; } = new();

    public BetTypePerformanceItem TotalItemLive { get; set; } = new();

    public BetTypePerformanceItem TotalItemNonLive { get; set; } = new();

    public void AddBetTypeList(IEnumerable<BetTypePerformanceInfo> betTypePerformanceInfos)
    {
        foreach (var item in betTypePerformanceInfos)
        {
            if (item != null)
            {
                AddItem(new BetTypePerformanceItem(item));
            }
        }
    }

    public void AddBetTypeList(IEnumerable<BetTypePerformanceInfo> betTypePerformanceInfos, bool isLive)
    {
        foreach (var item in betTypePerformanceInfos)
        {
            if (item != null)
            {
                AddItem(new BetTypePerformanceItem(item), isLive);
            }
        }
    }

    public void CalculateTotalMargin()
    {
        if (Total != null)
        {
            Total.Margin = CalculateTotalMargin(Total.WinLoss, Total.Turnover);
            Total.MarginReal = CalculateTotalMargin(Total.WinLossReal, Total.TurnoverReal);
        }

        if (TotalItemLive != null)
        {
            TotalItemLive.Margin = CalculateTotalMargin(TotalItemLive.WinLoss, TotalItemLive.Turnover);
            TotalItemLive.MarginReal = CalculateTotalMargin(TotalItemLive.WinLossReal, TotalItemLive.TurnoverReal);
        }

        if (TotalItemNonLive != null)
        {
            TotalItemNonLive.Margin = CalculateTotalMargin(TotalItemNonLive.WinLoss, TotalItemNonLive.Turnover);
            TotalItemNonLive.MarginReal = CalculateTotalMargin(TotalItemNonLive.WinLossReal, TotalItemNonLive.TurnoverReal);
        }
    }

    private void AddItem(BetTypePerformanceItem item)
    {
        ItemByOrderList.Add(item);

        CalculateTotal(item, item.IsLive);
    }

    private void AddItem(BetTypePerformanceItem item, bool isLive)
    {
        if (isLive)
        {
            ItemLiveList.Add(item);
        }
        else
        {
            ItemNonLiveList.Add(item);
        }

        CalculateTotal(item);
    }

    private void CalculateTotal(BetTypePerformanceItem item)
    {
        Total.NoOfTraders += item.NoOfTraders;
        Total.NoOfLeagues += item.NoOfLeagues;
        Total.NoOfMatches += item.NoOfMatches;
        Total.Turnover += item.Turnover;
        Total.WinLoss += item.WinLoss;
        Total.TurnoverReal += item.TurnoverReal;
        Total.WinLossReal += item.WinLossReal;
        Total.BetCount += item.BetCount;
    }

    private void CalculateTotal(BetTypePerformanceItem item, bool isLive)
    {
        if (isLive)
        {
            TotalItemLive.NoOfTraders += item.NoOfTraders;
            TotalItemLive.NoOfLeagues += item.NoOfLeagues;
            TotalItemLive.NoOfMatches += item.NoOfMatches;
            TotalItemLive.Turnover += item.Turnover;
            TotalItemLive.WinLoss += item.WinLoss;
            TotalItemLive.TurnoverReal += item.TurnoverReal;
            TotalItemLive.WinLossReal += item.WinLossReal;
            TotalItemLive.BetCount += item.BetCount;
        }
        else
        {
            TotalItemNonLive.NoOfTraders += item.NoOfTraders;
            TotalItemNonLive.NoOfLeagues += item.NoOfLeagues;
            TotalItemNonLive.NoOfMatches += item.NoOfMatches;
            TotalItemNonLive.Turnover += item.Turnover;
            TotalItemNonLive.WinLoss += item.WinLoss;
            TotalItemNonLive.TurnoverReal += item.TurnoverReal;
            TotalItemNonLive.WinLossReal += item.WinLossReal;
            TotalItemNonLive.BetCount += item.BetCount;
        }
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