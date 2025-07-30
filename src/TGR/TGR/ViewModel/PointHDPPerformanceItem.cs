namespace TGR.ViewModel;

public class PointHDPPerformanceItem
{
    public string PointHDPConvert { get; set; }

    public decimal PointHDP { get; set; }

    public int NoOfLeagues { get; set; }

    public int NoOfMatches { get; set; }

    public int BetCount { get; set; }

    public decimal Turnover { get; set; }

    public decimal WinLoss { get; set; }

    public decimal? Margin => Turnover != 0 ? WinLoss / Turnover * 100 : null;

    public decimal TurnoverReal { get; set; }

    public decimal WinLossReal { get; set; }

    public decimal? MarginReal => TurnoverReal != 0 ? WinLossReal / TurnoverReal * 100 : null;

    public static PointHDPPerformanceItem operator +(PointHDPPerformanceItem a, PointHDPPerformanceItem b)
    {
        return new PointHDPPerformanceItem
        {
            PointHDPConvert = a.PointHDPConvert,
            NoOfLeagues = a.NoOfLeagues + b.NoOfLeagues,
            NoOfMatches = a.NoOfMatches + b.NoOfMatches,
            Turnover = a.Turnover + b.Turnover,
            WinLoss = a.WinLoss + b.WinLoss,
            TurnoverReal = a.TurnoverReal + b.TurnoverReal,
            WinLossReal = a.WinLossReal + b.WinLossReal,
            BetCount = a.BetCount + b.BetCount
        };
    }
}
