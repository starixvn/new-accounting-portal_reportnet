namespace SoccerPerformanceDetail.ViewModel;

using Model;

public class TimeMachinePerformanceItem
{
    public TimeMachinePerformanceItem() { }

    public TimeMachinePerformanceItem(TimeMachinePerformanceInfo info, short betType)
    {
        Minute = ConvertMinute(info.HDP1, betType);
        NoOfLeagues = Convert.ToInt32(info.LeagueCount);
        NoOfMatches = Convert.ToInt32(info.MatchCount);
        BetCount = Convert.ToInt32(info.BetCount);
        Turnover = info.Turnover;
        WinLoss = info.WinLoss;
        Margin = info.Margin;
        TurnoverReal = info.TurnoverReal;
        WinLossReal = info.WinLossReal;
        MarginReal = info.MarginReal;
    }

    public string Minute { get; set; }

    public int NoOfLeagues { get; set; }

    public int NoOfMatches { get; set; }

    public int BetCount { get; set; }

    public decimal Turnover { get; set; }

    public decimal WinLoss { get; set; }

    public decimal? Margin { get; set; }

    public decimal TurnoverReal { get; set; }

    public decimal WinLossReal { get; set; }

    public decimal? MarginReal { get; set; }

    private string ConvertMinute(decimal hdp1, short betType)
    {
        var minute = betType switch
        {
            228 => "Before Minute " + (hdp1 + 1).ToString("N0"),
            229 when hdp1 < 10 => "00:00" + " - " + "0" + (hdp1 + 1).ToString("N0") + ":00",
            229 => "00:00" + " - " + (hdp1 + 1).ToString("N0") + ":00",
            _ => string.Empty
        };

        return minute;
    }
}
