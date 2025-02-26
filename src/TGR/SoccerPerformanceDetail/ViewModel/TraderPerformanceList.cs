// --------------------------------------------------------------------------
// <copyright file="TraderPerformanceList.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.5.17">Created</change>
// </history>

namespace SoccerPerformanceDetail.ViewModel;

using SoccerPerformanceDetail.Model;
using System;
using System.Collections.Generic;

public class TraderPerformanceList
{
    public TraderPerformanceList()
    {
        this.ItemLiveList = new List<TraderPerformanceItem>();
        this.ItemNonLiveList = new List<TraderPerformanceItem>();
        this.Total = new TraderPerformanceItem();
    }

    public List<TraderPerformanceItem> ItemLiveList { get; set; }

    public List<TraderPerformanceItem> ItemNonLiveList { get; set; }

    public TraderPerformanceItem Total { get; set; }
 
    public void AddTraderList(IEnumerable<TraderPerformanceInfo> traderPerformanceInfos, bool isLive)
    {
        foreach (var item in traderPerformanceInfos)
        {
            if (item != null)
            {
                this.AddItem(new TraderPerformanceItem(item), isLive);
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
            this.ItemLiveList.Add(item);
        }
        else
        {
            this.ItemNonLiveList.Add(item);
        }

        CalculateTotal(item);
    }

    private void CalculateTotal(TraderPerformanceItem item)
    {
        this.Total.NoOfLeagues += item.NoOfLeagues;
        this.Total.NoOfMatches += item.NoOfMatches;
        this.Total.Turnover += item.Turnover;
        this.Total.WinLoss += item.WinLoss;           
        this.Total.TurnoverReal += item.TurnoverReal;
        this.Total.WinLossReal += item.WinLossReal;
        this.Total.BetCount += item.BetCount;
    }
}