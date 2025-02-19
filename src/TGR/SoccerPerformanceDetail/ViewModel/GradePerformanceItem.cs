namespace SoccerPerformanceDetail.ViewModel;

using SoccerPerformanceDetail.Model;

public class GradePerformanceItem
{
    public GradePerformanceItem()
    {
    }

    public GradePerformanceItem(GradePerformanceInfo gradeData)
    {
        if (gradeData != null && gradeData.Grade.HasValue)
        {
            this.Grade = gradeData.Grade.Value;
            this.NoOfTraders = Convert.ToInt32(gradeData.TraderCount);
            this.NoOfLeagues = Convert.ToInt32(gradeData.LeagueCount);
            this.NoOfMatches = Convert.ToInt32(gradeData.MatchCount);
            this.Turnover = Convert.ToDecimal(gradeData.Turnover);
            this.WinLoss = Convert.ToDecimal(gradeData.WinLoss);                
            this.Margin = gradeData.Margin;
            this.TurnoverReal = Convert.ToDecimal(gradeData.TurnoverReal);
            this.WinLossReal = Convert.ToDecimal(gradeData.WinLossReal);
            this.MarginReal = gradeData.MarginReal;
            this.BetCount = Convert.ToInt32(gradeData.BetCount);
            this.Score = gradeData.Score;
            this.IsLive = gradeData.IsLive;
        }
    }

    public int Grade { get; set; }

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