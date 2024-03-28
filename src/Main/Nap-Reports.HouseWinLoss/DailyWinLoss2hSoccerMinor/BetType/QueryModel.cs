using System;
using Nap_Reports.HouseWinLoss.Configuration;

namespace Nap_Reports.HouseWinLoss.DailyWinLoss2hSoccerMinor.BetType
{
    public class QueryModel
    {
        private const int AllLiveAndNonLive = 2;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool NormalMode { get; set; } = true;

        public int IsLive { get; set; } = AllLiveAndNonLive;

        public string SortColumnName { get; set; }

        public string SortDirection { get; set; }

        public int IsSabaSport { get; set; } = Constants.IsSabaSportDefault;
    }
}