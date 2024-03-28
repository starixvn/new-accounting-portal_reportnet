using System;

namespace Nap_Reports.HouseWinLoss.DailyWinLoss2hSoccerMinor.SportType
{
    public class QueryModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool NormalMode { get; set; } = true;
    }
}