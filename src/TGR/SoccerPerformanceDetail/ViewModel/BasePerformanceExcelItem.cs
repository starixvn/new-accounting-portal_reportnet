using Epplus.Mapper.Annotations;

namespace SoccerPerformanceDetail.ViewModel;

public class BasePerformanceExcelItem
{
    [Cell("A5")]
    public int No { get; set; }

    [Cell("C5")]
    public string Market { get; set; }

    public bool IsLive { get; set; }

    [Cell("K5")]
    public decimal? Margin { get; set; }

    [Cell("O5")]
    public decimal? MarginReal { get; set; }

    [Cell("F5")]
    public int NoOfMatches { get; set; }

    [Cell("H5")]
    public decimal Turnover { get; set; }

    [Cell("L5")]
    public decimal TurnoverReal { get; set; }
} 