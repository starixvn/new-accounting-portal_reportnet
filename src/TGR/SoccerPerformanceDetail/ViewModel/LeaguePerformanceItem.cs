using NPOI.SS.Formula.Functions;

namespace SoccerPerformanceDetail.ViewModel;

public class LeaguePerformanceItem
{
    public int LeagueId { get; set; }

    public string LeagueName { get; set; }

    public int LeagueGroupId { get; set; }

    public string LeagueGroupName { get; set; }

    public int NoOfTraders { get; set; }

    public int NoOfMatches { get; set; }

    public int NoOfBetChoices { get; set; }

    public decimal Turnover { get; set; }

    public decimal WinLoss { get; set; }

    public decimal? Margin => Turnover != 0 ? WinLoss / Turnover * 100 : null;

    public int BetCount { get; set; }

    public decimal TurnoverReal { get; set; }

    public decimal WinLossReal { get; set; }

    public decimal? MarginReal => TurnoverReal != 0 ? WinLossReal / TurnoverReal* 100 : null;

    public decimal? BetTypeBMShift1 { get; set; }

    public decimal? BetTypeBMShift2 { get; set; }

    public decimal? BetTypeBMShift3 { get; set; }

    public string Market { get; set; }
       

    public bool IsLive { get; set; }

    public int FlagForViewReport { get; set; }

    public static LeaguePerformanceItem operator +(LeaguePerformanceItem a, LeaguePerformanceItem b)
    {
        return new LeaguePerformanceItem
        {
            NoOfTraders = a.NoOfTraders + b.NoOfTraders,
            NoOfMatches = a.NoOfMatches + b.NoOfMatches,
            NoOfBetChoices = a.NoOfBetChoices + b.NoOfBetChoices,
            Turnover = a.Turnover + b.Turnover,
            WinLoss = a.WinLoss + b.WinLoss,
            TurnoverReal = a.TurnoverReal + b.TurnoverReal,
            WinLossReal = a.WinLossReal + b.WinLossReal,
            BetCount = a.BetCount + b.BetCount
        };
    }
}