namespace SoccerPerformanceDetail.ViewModel;

using Model;

public class TeamPerformanceItem
{
    public TeamPerformanceItem()
    {
    }

    public TeamPerformanceItem(TeamPerformanceInfo teamData)
    {
        if (teamData != null && teamData.TeamId.HasValue)
        {
            TeamId = teamData.TeamId.Value;
            TeamName = teamData.TeamName;
            NoOfTraders = Convert.ToInt32(teamData.TraderCount);
            NoOfLeagues = Convert.ToInt32(teamData.LeagueCount);
            NoOfMatches = Convert.ToInt32(teamData.MatchCount);
            Turnover = Convert.ToDecimal(teamData.Turnover);
            WinLoss = Convert.ToDecimal(teamData.WinLoss);               
            Margin = teamData.Margin;
            TurnoverReal = Convert.ToDecimal(teamData.TurnoverReal);
            WinLossReal = Convert.ToDecimal(teamData.WinLossReal);
            MarginReal = teamData.MarginReal;
            BetCount = Convert.ToInt32(teamData.BetCount);
            Score = teamData.Score;
            IsLive = teamData.IsLive;
        }
    }

    public int TeamId { get; set; }

    public string TeamName { get; set; }

    public int NoOfTraders { get; set; }

    public int NoOfLeagues { get; set; }

    public int NoOfMatches { get; set; }

    public decimal Turnover { get; set; }

    public decimal WinLoss { get; set; }        

    public decimal? Margin { get; set; }

    public decimal TurnoverReal { get; set; }

    public decimal WinLossReal { get; set; }

    public decimal? MarginReal { get; set; }

    public int BetCount { get; set; }

    public decimal? Score { get; set; }

    public bool IsLive { get; set; }
}