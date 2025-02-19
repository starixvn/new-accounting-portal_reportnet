namespace SoccerPerformanceDetail.ViewModel;

using SoccerPerformanceDetail.Model;
using System;

/// <summary>
/// Trader performance view model.
/// </summary>
[Serializable]
public class TraderPerformanceItem
{
    public TraderPerformanceItem()
    {
    }

    public TraderPerformanceItem(TraderPerformanceInfo traderData)
    {
        if (traderData != null && traderData.CustId.HasValue)
        {
            this.TraderId = traderData.CustId.Value;
            this.TraderName = traderData.Username;
            this.Grade = Convert.ToInt32(traderData.Grade);
            this.HasGradeHistory = Convert.ToInt32(traderData.NoOfGradeChanged) > 1;
            this.TeamId = Convert.ToInt32(traderData.TeamId);
            this.TeamName = traderData.TeamName;
            this.HasTeamHistory = Convert.ToInt32(traderData.NoOfTeamChanged) > 1;
            this.NoOfLeagues = Convert.ToInt32(traderData.LeagueCount);
            this.NoOfMatches = Convert.ToInt32(traderData.MatchCount);
            this.BetCount = Convert.ToInt32(traderData.BetCount);
            this.Turnover = Convert.ToDecimal(traderData.Turnover);
            this.WinLoss = Convert.ToDecimal(traderData.WinLoss);               
            this.Margin = traderData.Margin;
            this.TurnoverReal = Convert.ToDecimal(traderData.TurnoverReal);
            this.WinLossReal = Convert.ToDecimal(traderData.WinLossReal);
            this.MarginReal = traderData.MarginReal;
            this.Score = traderData.Score;
            this.IsLive = traderData.IsLive;
        }
    }

    public int TraderId { get; set; }

    public string TraderName { get; set; }

    public int Grade { get; set; }

    public bool HasGradeHistory { get; set; }

    public int TeamId { get; set; }

    public string TeamName { get; set; }

    public bool HasTeamHistory { get; set; }

    public int NoOfLeagues { get; set; }

    public int NoOfMatches { get; set; }

    public int BetCount { get; set; }

    public decimal Turnover { get; set; }

    public decimal WinLoss { get; set; }
   
    public decimal? Margin { get; set; }

    public decimal TurnoverReal { get; set; }

    public decimal WinLossReal { get; set; }

    public decimal? MarginReal { get; set; }

    public decimal? Score { get; set; }

    public bool IsLive { get; set; }
}