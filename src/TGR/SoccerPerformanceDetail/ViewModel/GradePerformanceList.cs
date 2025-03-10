// --------------------------------------------------------------------------
// <copyright file="GradePerformanceList.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.5.17">Created</change>
// </history>

namespace SoccerPerformanceDetail.ViewModel;

using Model;

/// <summary>
/// Grade performance list view model.
/// </summary>
[Serializable]
public class GradePerformanceList
{
    public List<GradePerformanceItem> ItemLiveList { get; set; } = [];

    public List<GradePerformanceItem> ItemNonLiveList { get; set; } = [];

    public GradePerformanceItem Total { get; set; } = new();

    public void AddGradeList(IEnumerable<GradePerformanceInfo> gradePerformanceInfos, bool isLive)
    {
        foreach (var item in gradePerformanceInfos)
        {
            if (item != null)
            {
                AddItem(new GradePerformanceItem(item), isLive);
            }
        }
    }
       
    private void AddItem(GradePerformanceItem item, bool isLive)
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

    private void CalculateTotal(GradePerformanceItem item)
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