namespace LeagueWinLoss.Model
{
    public class MatchWinLossItem
    {
        public MatchWinLossByLiveStatus Total { get; set; }

        public List<MatchWinLossByLiveStatus> Matches { get; set; }
    }
}