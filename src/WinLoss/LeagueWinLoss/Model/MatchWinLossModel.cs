namespace LeagueWinLoss.Model
{
    public class MatchWinLossModel
    {
        public List<MatchWinLossItem> Match { get; set; }

        public MatchWinLossByLiveStatus Total { get; set; }

        public MatchWinLossByLiveStatus RM { get; set; }

        public MatchWinLossByLiveStatus BARM { get; set; }

        public MatchWinLossByLiveStatus GrandTotal { get; set; }
    }
}