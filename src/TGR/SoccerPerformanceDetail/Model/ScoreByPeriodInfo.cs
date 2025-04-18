
namespace SoccerPerformanceDetail.Model;

public class ScoreByPeriodInfo
{
    public short? BetType { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public decimal? Benchmark { get; set; }

    public decimal? Score { get; set; }
}
