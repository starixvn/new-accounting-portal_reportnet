namespace SoccerPerformanceDetail.ViewModel;

using Model;

public class LeaguePerformanceList
{
    public List<LeaguePerformanceItem> ItemByOrderList { get; set; } = [];

    public List<LeaguePerformanceItem> ItemLiveList { get; set; } = [];

    public List<LeaguePerformanceItem> ItemNonLiveList { get; set; } = [];

    public LeaguePerformanceItem Total { get; set; } = new();

    public LeaguePerformanceItem TotalItemLive { get; set; } = new();

    public LeaguePerformanceItem TotalItemNonLive { get; set; } = new();

    public void AddLeagueList(IEnumerable<LeaguePerformanceInfo> leaguePerformanceInfos, IEnumerable<LeagueGroup> leagueGroups)
    {
        foreach (var item in leaguePerformanceInfos)
        {
            if (item != null)
            {
                var leagueGroupName = string.Empty;
                if (leagueGroups != null && item.LeagueGroupID.HasValue)
                {
                    var group = leagueGroups
                        .Where(x => x.TargetLeagueGroupId.Equals(item.LeagueGroupID.Value))
                        .Select(x => x.LeagueGroupName).ToList();

                    leagueGroupName = group.Any() ? group[0] : "-";
                }
                var itemData = new LeaguePerformanceItem(item)
                {
                    LeagueGroupName = leagueGroupName
                };

                AddItem(itemData);
            }
        }
    }

    public void AddLeagueList(IEnumerable<LeaguePerformanceInfo> leaguePerformanceInfos, IEnumerable<LeagueGroup> leagueGroups, bool isLive)
    {
        foreach (var item in leaguePerformanceInfos)
        {
            if (item != null)
            {
                var leagueGroupName = string.Empty;
                if (leagueGroups != null && item.LeagueGroupID.HasValue)
                {
                    var group = leagueGroups
                        .Where(x => x.TargetLeagueGroupId.Equals(item.LeagueGroupID.Value))
                        .Select(x => x.LeagueGroupName).ToList();

                    leagueGroupName = group.Any() ? group[0] : "-";
                }
                var itemData = new LeaguePerformanceItem(item)
                {
                    LeagueGroupName = leagueGroupName
                };

                AddItem(itemData, isLive);
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

    private void AddItem(LeaguePerformanceItem item)
    {
        ItemByOrderList.Add(item);

        CalculateTotal(item, item.IsLive);
    }

    private void AddItem(LeaguePerformanceItem item, bool isLive)
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

    private void CalculateTotal(LeaguePerformanceItem item)
    {
        Total.NoOfTraders += item.NoOfTraders;
        Total.NoOfMatches += item.NoOfMatches;
        Total.NoOfBetChoices += item.NoOfBetChoices;
        Total.Turnover += item.Turnover;
        Total.WinLoss += item.WinLoss;
        Total.TurnoverReal += item.TurnoverReal;
        Total.WinLossReal += item.WinLossReal;
        Total.BetCount += item.BetCount;
    }

    private void CalculateTotal(LeaguePerformanceItem item, bool isLive)
    {
        if (isLive)
        {
            TotalItemLive.NoOfTraders += item.NoOfTraders;
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