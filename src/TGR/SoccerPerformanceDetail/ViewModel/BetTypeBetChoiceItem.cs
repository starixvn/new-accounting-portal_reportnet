namespace SoccerPerformanceDetail.ViewModel;

using Model;

public class BetTypeBetChoiceItem
{
    public BetTypeBetChoiceItem() { }

    public BetTypeBetChoiceItem(BetTypeBetChoicePerformanceInfo info)
    {          
        BetChoice = info.BetChoice;
        BetOption = info.BetOption;
        BetTeam = info.BetTeam;
        OddsSpread = info.OddsSpread;
        LeagueCount = info.LeagueCount;
        MatchCount = info.MatchCount;
        BetCount = info.BetCount;
        Turnover = info.Turnover;
        WinLoss = info.WinLoss;
        Margin = info.Margin;
        TurnoverReal = info.TurnoverReal;
        WinLossReal = info.WinLossReal;
        MarginReal = info.MarginReal;
        IsLive = info.IsLive;
    }

    public string BetChoice { get; set; }

    public string BetOption { get; set; }

    public string BetTeam { get; set; }

    public decimal OddsSpread { get; set; }

    public int LeagueCount { get; set; }

    public int MatchCount { get; set; }

    public int BetCount { get; set; }

    public decimal Turnover { get; set; }

    public decimal WinLoss { get; set; }

    public decimal? Margin { get; set; }

    public decimal TurnoverReal { get; set; }

    public decimal WinLossReal { get; set; }

    public decimal? MarginReal { get; set; }

    public bool IsLive { get; set; }
}
