namespace LeagueWinLoss.Model
{
    public class BetTypeWinLossNonLiveModel
    {
        public List<BettypeWinLossBySite> BettypeWinLoss { get; set; }

        public BettypeWinLossBySite Total { get; set; }

        public BettypeWinLossBySite RM { get; set; }

        public BettypeWinLossBySite BARM { get; set; }

        public BettypeWinLossBySite GrandTotal { get; set; }
    }
}
