namespace TGR.Model;

public class BetTypeBetChoicePerformanceInfo
{       
    public string BetChoice { get; set; }

    public string BetOption { get; set; }

    public string BetTeam { get; set; }

    public decimal Hdp2 { get; set; }

    public decimal OddsSpread { get; set; }

    public int LeagueCount { get; set; }

    public int MatchCount { get; set; }

    public int BetCount { get; set; }

    public decimal Turnover { get; set; }

    public decimal WinLoss { get; set; }     

    public decimal? Margin => Turnover != 0 ? WinLoss / Turnover * 100 : null;

    public decimal TurnoverReal { get; set; }

    public decimal WinLossReal { get; set; }

    public decimal? MarginReal => TurnoverReal != 0 ? WinLossReal / TurnoverReal * 100 : null;

    public bool IsLive { get; set; }        
}