namespace LeagueWinLoss.Model;

public class BettypeWinLossWithChoiceModel
{
    public List<BetTypeWinLossItemByChoice> BettypeWinLoss { get; set; }

    public BettypeWinLossBySite Total { get; set; }

    public BettypeWinLossBySite RM { get; set; }

    public BettypeWinLossBySite BARM { get; set; }

    public BettypeWinLossBySite GrandTotal { get; set; }
}