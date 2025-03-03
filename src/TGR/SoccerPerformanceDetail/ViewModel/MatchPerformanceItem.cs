namespace SoccerPerformanceDetail.ViewModel
{
    using SoccerPerformanceDetail.Model;
    using Utilities.DateTimes;

    public class MatchPerformanceItem
    {
        public MatchPerformanceItem()
        {
        }

        public MatchPerformanceItem(MatchPerformanceInfo matchData)
        {
            if (matchData != null && matchData.MatchId.HasValue)
            {
                this.MatchId = matchData.MatchId.Value;
                this.MatchName = string.Empty; //string.Format("{0} {1} {2}", CacheRef.GetTeamName(matchData.HomeId), ViewLang.Vs, CacheRef.GetTeamName(matchData.AwayId));
                this.EventDate = Convert.ToDateTime(matchData.EventDate).ToShortFormatedDate();
                this.Turnover = Convert.ToDecimal(matchData.Turnover);
                this.WinLoss = Convert.ToDecimal(matchData.WinLoss);
                this.BetCount = Convert.ToInt32(matchData.BetCount);
                this.Margin = matchData.Margin;
                this.TurnoverReal = Convert.ToDecimal(matchData.TurnoverReal);
                this.WinLossReal = Convert.ToDecimal(matchData.WinLossReal);
                this.MarginReal = matchData.MarginReal;
                this.Score = matchData.Score;
                this.IsLive = matchData.IsLive;
            }
        }

        public MatchPerformanceItem(MatchPerformanceRawInfo matchData)
        {
            if (matchData != null && matchData.MatchId.HasValue)
            {
                this.MatchId = matchData.MatchId.Value;
                this.MatchName = string.Empty; // string.Format("{0} {1} {2}", CacheRef.GetTeamName(matchData.HomeId), ViewLang.Vs, CacheRef.GetTeamName(matchData.AwayId));
                this.EventDate = Convert.ToDateTime(matchData.EventDate).ToShortFormatedDate();
                this.BetCount = Convert.ToInt32(matchData.BetCount);
                this.Turnover = Convert.ToDecimal(matchData.Turnover);
                this.WinLoss = Convert.ToDecimal(matchData.WinLoss);
                this.Margin = matchData.Margin;
                this.TurnoverReal = Convert.ToDecimal(matchData.TurnoverReal);
                this.WinLossReal = Convert.ToDecimal(matchData.WinLossReal);
                this.MarginReal = matchData.MarginReal;
                this.Score = matchData.SumCategory == 0 ? null : (matchData.WinLossScore * 100 / matchData.SumCategory);
                this.WinLossScore = Convert.ToDecimal(matchData.WinLossScore);
                this.SumCategory = Convert.ToDecimal(matchData.SumCategory);
            }
        }

        public int MatchId { get; set; }

        public string MatchName { get; set; }

        public string EventDate { get; set; }

        public int BetCount { get; set; }

        public decimal Turnover { get; set; }

        public decimal WinLoss { get; set; }

        public decimal? Margin { get; set; }

        public decimal TurnoverReal { get; set; }

        public decimal WinLossReal { get; set; }

        public decimal? MarginReal { get; set; }

        public decimal? Score { get; set; }

        public decimal WinLossScore { get; set; }

        public decimal SumCategory { get; set; }

        public bool IsLive { get; set; }
    }
}