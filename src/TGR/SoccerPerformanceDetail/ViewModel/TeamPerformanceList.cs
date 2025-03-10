// --------------------------------------------------------------------------
// <copyright file="TeamPerformanceList.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.5.17">Created</change>
// </history>

namespace SoccerPerformanceDetail.ViewModel;

using Model;

/// <summary>
/// League performance list view model.
/// </summary>
[Serializable]
public class TeamPerformanceList
{
    public List<TeamPerformanceItem> ItemLiveList { get; set; } = [];

    public List<TeamPerformanceItem> ItemNonLiveList { get; set; } = [];

    public TeamPerformanceItem Total { get; set; } = new();

    public void AddTeamList(IEnumerable<TeamPerformanceInfo> teamPerformanceInfos, bool isLive)
    {
        foreach (var item in teamPerformanceInfos)
        {
            if (item != null)
            {
                AddItem(new TeamPerformanceItem(item), isLive);
            }
        }
    }

    private void AddItem(TeamPerformanceItem item, bool isLive)
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

    private void CalculateTotal(TeamPerformanceItem item)
    {
        Total.NoOfTraders += item.NoOfTraders;
        Total.NoOfLeagues += item.NoOfLeagues;
        Total.NoOfMatches += item.NoOfMatches;
        Total.Turnover += item.Turnover;
        Total.WinLoss += item.WinLoss;           
        Total.Margin += item.Margin;
        Total.TurnoverReal += item.TurnoverReal;
        Total.WinLossReal += item.WinLossReal;
        Total.MarginReal += item.MarginReal;
        Total.BetCount += item.BetCount;
    }
}