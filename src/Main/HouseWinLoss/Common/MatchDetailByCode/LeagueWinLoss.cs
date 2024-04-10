using HouseWinLoss.Common.Models;

namespace HouseWinLoss.Common.MatchDetailByCode
{
    public class LeagueWinLoss
    {
        public string Name { get; set; }

        public List<WinLoss> Matches { get; set; }

        public WinLoss Total { get; set; }

        public int TotalRows { get; set; }
    }
}