using System.Collections.Generic;

namespace Nap_Reports.HouseWinLoss.Common.MatchDetail
{
    public class WinLossModel
    {
        public List<LeagueWinLoss> Leagues { get; set; }

        public WinLoss Total { get; set; }

        public List<WinLoss> RM { get; set; } = new List<WinLoss>();

        public int TotalRows { get; set; }
    }
}