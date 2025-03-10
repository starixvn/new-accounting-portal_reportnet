namespace SoccerPerformanceDetail.ViewModel;

using Model;

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
            TraderId = traderData.CustId.Value;
            TraderName = traderData.Username;
            Grade = Convert.ToInt32(traderData.Grade);
            HasGradeHistory = Convert.ToInt32(traderData.NoOfGradeChanged) > 1;
            TeamId = Convert.ToInt32(traderData.TeamId);
            TeamName = traderData.TeamName;
            HasTeamHistory = Convert.ToInt32(traderData.NoOfTeamChanged) > 1;
            NoOfLeagues = Convert.ToInt32(traderData.LeagueCount);
            NoOfMatches = Convert.ToInt32(traderData.MatchCount);
            BetCount = Convert.ToInt32(traderData.BetCount);
            Turnover = Convert.ToDecimal(traderData.Turnover);
            WinLoss = Convert.ToDecimal(traderData.WinLoss);               
            Margin = traderData.Margin;
            TurnoverReal = Convert.ToDecimal(traderData.TurnoverReal);
            WinLossReal = Convert.ToDecimal(traderData.WinLossReal);
            MarginReal = traderData.MarginReal;
            Score = traderData.Score;
            IsLive = traderData.IsLive;
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