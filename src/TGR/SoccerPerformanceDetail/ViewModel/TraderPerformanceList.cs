// --------------------------------------------------------------------------
// <copyright file="TraderPerformanceList.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.5.17">Created</change>
// </history>

namespace SoccerPerformanceDetail.ViewModel;

using Model;

public class TraderPerformanceList
{
    public List<TraderPerformanceItem> ItemLiveList { get; set; } = [];

    public List<TraderPerformanceItem> ItemNonLiveList { get; set; } = [];

    public TraderPerformanceItem Total { get; set; } = new();

    public void AddTraderList(IEnumerable<TraderPerformanceInfo> traderPerformanceInfos, bool isLive)
    {
        foreach (var item in traderPerformanceInfos)
        {
            if (item != null)
            {
                AddItem(new TraderPerformanceItem(item), isLive);
            }                
        }
    }

    public void CalculateTotalMargin()
    {
        Total.Margin = Total.Turnover != 0 ? Total.WinLoss / Total.Turnover : null;
        Total.MarginReal = Total.TurnoverReal != 0 ? Total.WinLossReal / Total.TurnoverReal : null;
    }

    private void AddItem(TraderPerformanceItem item, bool isLive)
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

    private void CalculateTotal(TraderPerformanceItem item)
    {
        Total.NoOfLeagues += item.NoOfLeagues;
        Total.NoOfMatches += item.NoOfMatches;
        Total.Turnover += item.Turnover;
        Total.WinLoss += item.WinLoss;           
        Total.TurnoverReal += item.TurnoverReal;
        Total.WinLossReal += item.WinLossReal;
        Total.BetCount += item.BetCount;
    }
}