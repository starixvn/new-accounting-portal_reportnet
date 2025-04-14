namespace SoccerPerformanceDetail.ViewModel
{
    using SoccerPerformanceDetail.Model;
    using System.Collections.Generic;

    public class PointHDPPerformanceList
    {
        public PointHDPPerformanceList()
        {
            this.ItemList = new List<PointHDPPerformanceItem>();
            this.Total = new PointHDPPerformanceItem();
        }

        public List<PointHDPPerformanceItem> ItemList { get; set; }

        public PointHDPPerformanceItem Total { get; set; }

        public bool IsTotalGoalBetType { get; set; }

        public void AddItem(IEnumerable<PointHDPPerformanceInfo> infos, short betType, string betTeam)
        {
            var betTypeStr = betType.ToString();
            IsTotalGoalBetType = betTypeStr == "228" || betTypeStr == "229";

            var betOption = ConvertOption(betTeam);

            foreach (var info in infos)
            {
                if (info != null)
                {
                    var item = new PointHDPPerformanceItem(info, betType, betOption);

                    this.ItemList.Add(item);

                    CalculateTotal(item);
                }
            }
        }

        private string ConvertOption(string betTeam)
        {
            if (betTeam.ToUpper() == "HOME")
            {
                return "1";
            }
            else if (betTeam.ToUpper() == "AWAY")
            {
                return "2";
            }
            else
            {
                return betTeam;
            }
        }

        private void CalculateTotal(PointHDPPerformanceItem item)
        {
            this.Total.NoOfLeagues += item.NoOfLeagues;
            this.Total.NoOfMatches += item.NoOfMatches;
            this.Total.Turnover += item.Turnover;
            this.Total.WinLoss += item.WinLoss;
            this.Total.TurnoverReal += item.TurnoverReal;
            this.Total.WinLossReal += item.WinLossReal;
            this.Total.BetCount += item.BetCount;
        }
    }
}
