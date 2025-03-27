namespace SoccerPerformanceDetail.ViewModel;

using Model;

public class GradePerformanceItem
{
    public GradePerformanceItem()
    {
    }

    public GradePerformanceItem(GradePerformanceInfo gradeData)
    {
        if (gradeData is { Grade: not null })
        {
            Grade = gradeData.Grade.Value;
            NoOfTraders = Convert.ToInt32(gradeData.TraderCount);
            NoOfLeagues = Convert.ToInt32(gradeData.LeagueCount);
            NoOfMatches = Convert.ToInt32(gradeData.MatchCount);
            Turnover = Convert.ToDecimal(gradeData.Turnover);
            WinLoss = Convert.ToDecimal(gradeData.WinLoss);                
            Margin = gradeData.Margin;
            TurnoverReal = Convert.ToDecimal(gradeData.TurnoverReal);
            WinLossReal = Convert.ToDecimal(gradeData.WinLossReal);
            MarginReal = gradeData.MarginReal;
            BetCount = Convert.ToInt32(gradeData.BetCount);
            Score = gradeData.Score;
            IsLive = gradeData.IsLive;
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