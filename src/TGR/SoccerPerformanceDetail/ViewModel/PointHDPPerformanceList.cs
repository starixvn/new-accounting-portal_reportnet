namespace SoccerPerformanceDetail.ViewModel
{
    using System.Collections.Generic;

    public class PointHDPPerformanceList
    {
        public List<PointHDPPerformanceItem> ItemList { get; set; } = [];

        public PointHDPPerformanceItem Total { get; set; } = new();

        public bool IsTotalGoalBetType { get; set; }
    }
}
