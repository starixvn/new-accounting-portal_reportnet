namespace LeagueWinLoss.Model
{
    public class SiteWinLossModel
    {
        public List<SiteWinLossItem> Sites { get; set; }

        public SiteWinLossItem Total { get; set; }

        public SiteWinLossItem RM { get; set; }

        public SiteWinLossItem BARM { get; set; }

        public SiteWinLossItem GrandTotal { get; set; }
    }
}