using HouseWinLoss.Common.Models;

namespace HouseWinLoss.Common.MatchDetailByCode
{
    public class WinLossModel
    {
        public List<LeagueWinLoss> Leagues { get; set; }

        public WinLoss Total { get; set; }

        public int TotalRows { get; set; }
    }
}