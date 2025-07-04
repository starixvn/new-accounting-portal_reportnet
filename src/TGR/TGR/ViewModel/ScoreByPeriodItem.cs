using TGR.Model;

namespace TGR.ViewModel;

public class ScoreByPeriodItem(ScoreByPeriodInfo scoreInfo)
{
    public short BetType { get; set; } = Convert.ToInt16(scoreInfo.BetType);

    public string FromDate { get; set; } = Convert.ToDateTime(scoreInfo.FromDate).ToString("MM/dd/yyyy");

    public string ToDate { get; set; } = Convert.ToDateTime(scoreInfo.ToDate).ToString("MM/dd/yyyy");

    public decimal? Benchmark { get; set; } = scoreInfo.Benchmark;

    public decimal? Score { get; set; } = scoreInfo.Score;
}