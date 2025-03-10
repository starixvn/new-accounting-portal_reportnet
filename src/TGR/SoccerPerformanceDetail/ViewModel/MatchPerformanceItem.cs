namespace SoccerPerformanceDetail.ViewModel;

using global::Utilities.DateTimes;
using Model;

public class MatchPerformanceItem
{
    public MatchPerformanceItem()
    {
    }

    public MatchPerformanceItem(MatchPerformanceInfo matchData)
    {
        if (matchData != null && matchData.MatchId.HasValue)
        {
            MatchId = matchData.MatchId.Value;
            MatchName = string.Empty; //string.Format("{0} {1} {2}", CacheRef.GetTeamName(matchData.HomeId), ViewLang.Vs, CacheRef.GetTeamName(matchData.AwayId));
            EventDate = Convert.ToDateTime(matchData.EventDate).ToShortFormatedDate();
            Turnover = Convert.ToDecimal(matchData.Turnover);
            WinLoss = Convert.ToDecimal(matchData.WinLoss);
            BetCount = Convert.ToInt32(matchData.BetCount);
            Margin = matchData.Margin;
            TurnoverReal = Convert.ToDecimal(matchData.TurnoverReal);
            WinLossReal = Convert.ToDecimal(matchData.WinLossReal);
            MarginReal = matchData.MarginReal;
            Score = matchData.Score;
            IsLive = matchData.IsLive;
        }
    }

    public MatchPerformanceItem(MatchPerformanceRawInfo matchData)
    {
        if (matchData != null && matchData.MatchId.HasValue)
        {
            MatchId = matchData.MatchId.Value;
            MatchName = string.Empty; // string.Format("{0} {1} {2}", CacheRef.GetTeamName(matchData.HomeId), ViewLang.Vs, CacheRef.GetTeamName(matchData.AwayId));
            EventDate = Convert.ToDateTime(matchData.EventDate).ToShortFormatedDate();
            BetCount = Convert.ToInt32(matchData.BetCount);
            Turnover = Convert.ToDecimal(matchData.Turnover);
            WinLoss = Convert.ToDecimal(matchData.WinLoss);
            Margin = matchData.Margin;
            TurnoverReal = Convert.ToDecimal(matchData.TurnoverReal);
            WinLossReal = Convert.ToDecimal(matchData.WinLossReal);
            MarginReal = matchData.MarginReal;
            Score = matchData.SumCategory == 0 ? null : matchData.WinLossScore * 100 / matchData.SumCategory;
            WinLossScore = Convert.ToDecimal(matchData.WinLossScore);
            SumCategory = Convert.ToDecimal(matchData.SumCategory);
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