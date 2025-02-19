namespace SoccerPerformanceDetail.ViewModel;

using SoccerPerformanceDetail.Model;

public class ScoreByPeriodItem
{
    public ScoreByPeriodItem(ScoreByPeriodInfo scoreInfo)
    {
        this.BetType = Convert.ToInt16(scoreInfo.BetType);
        this.Benchmark = scoreInfo.Benchmark;
        this.FromDate = Convert.ToDateTime(scoreInfo.FromDate).ToString("MM/dd/yyyy");
        this.ToDate = Convert.ToDateTime(scoreInfo.ToDate).ToString("MM/dd/yyyy");
        this.Score = scoreInfo.Score;
    }

    public short BetType { get; set; }

    public string FromDate { get; set; }

    public string ToDate { get; set; }

    public decimal? Benchmark { get; set; }

    public decimal? Score { get; set; }
}