namespace CashOutPerformance.Models;

public class MatchDetailLevelModel : CommonTicketModel
{
    public DateTime WLDate { get; set; }

    public string Username { get; set; }

    public string Currency { get; set; }

    public string OrigID { get; set; }

    public string BuyBackID { get; set; }

    public string CashOutID { get; set; }

    public int SportType { get; set; }

    public string SportName { get; set; }

    public string LeagueName { get; set; }

    public string MatchName { get; set; }

    public DateTime BetTime { get; set; }

    public DateTime CashOutTime { get; set; }

    public string BetOdd { get; set; }

    public string BetTypeName { get; set; }

    public string BetChoice { get; set; }

    public decimal COTurnover { get; set; }

    public decimal BBAmount { get; set; }

    public decimal MemberWL { get; set; }

    public string BetScore { get; set; }

    public string BetSection { get; set; }

    public string CurrentSection { get; set; }

    public string CurrentGoalLine { get; set; }

    public string CurrentHomeOdds { get; set; }

    public string CurrentAwayOdds { get; set; }

    public string CurrentScore { get; set; }

    public string HalfScore { get; set; }

    public string FinalScore { get; set; }

    public string SiteGroup { get; set; }
}