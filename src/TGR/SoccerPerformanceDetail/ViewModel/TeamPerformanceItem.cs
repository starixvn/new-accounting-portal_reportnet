namespace SoccerPerformanceDetail.ViewModel;

using SoccerPerformanceDetail.Model;

public class TeamPerformanceItem
{
    public TeamPerformanceItem()
    {
    }

    public TeamPerformanceItem(TeamPerformanceInfo teamData)
    {
        if (teamData != null && teamData.TeamId.HasValue)
        {
            this.TeamId = teamData.TeamId.Value;
            this.TeamName = teamData.TeamName;
            this.NoOfTraders = Convert.ToInt32(teamData.TraderCount);
            this.NoOfLeagues = Convert.ToInt32(teamData.LeagueCount);
            this.NoOfMatches = Convert.ToInt32(teamData.MatchCount);
            this.Turnover = Convert.ToDecimal(teamData.Turnover);
            this.WinLoss = Convert.ToDecimal(teamData.WinLoss);               
            this.Margin = teamData.Margin;
            this.TurnoverReal = Convert.ToDecimal(teamData.TurnoverReal);
            this.WinLossReal = Convert.ToDecimal(teamData.WinLossReal);
            this.MarginReal = teamData.MarginReal;
            this.BetCount = Convert.ToInt32(teamData.BetCount);
            this.Score = teamData.Score;
            this.IsLive = teamData.IsLive;
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