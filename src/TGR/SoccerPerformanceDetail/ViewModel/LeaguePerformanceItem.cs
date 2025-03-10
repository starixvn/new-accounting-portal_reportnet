namespace SoccerPerformanceDetail.ViewModel;

using Model;

public class LeaguePerformanceItem
{
    public LeaguePerformanceItem()
    {
    }

    public LeaguePerformanceItem(LeaguePerformanceInfo leagueData)
    {
        if (leagueData != null && leagueData.LeagueId.HasValue)
        {
            LeagueId = leagueData.LeagueId.Value;
            LeagueName = leagueData.LeagueName;
            LeagueGroupId = Convert.ToInt32(leagueData.LeagueGroupID);
            NoOfTraders = Convert.ToInt32(leagueData.TraderCount);
            NoOfMatches = Convert.ToInt32(leagueData.MatchCount);
            NoOfBetChoices = Convert.ToInt32(leagueData.BetChoiceCount);
            Turnover = Convert.ToDecimal(leagueData.Turnover);
            WinLoss = Convert.ToDecimal(leagueData.WinLoss);
            Margin = leagueData.Margin;
            TurnoverReal = Convert.ToDecimal(leagueData.TurnoverReal);
            WinLossReal = Convert.ToDecimal(leagueData.WinLossReal);
            MarginReal = leagueData.MarginReal;
            BetCount = Convert.ToInt32(leagueData.BetCount);
            BetTypeBMShift1 = leagueData.BetTypeBMShift1;
            BetTypeBMShift2 = leagueData.BetTypeBMShift2;
            BetTypeBMShift3 = leagueData.BetTypeBMShift3;              
            Market = GetMarketToString(leagueData.Market);
            IsLive = leagueData.IsLive;
            FlagForViewReport = leagueData.FlagForViewReport;
        }
    }

    public int LeagueId { get; set; }

    public string LeagueName { get; set; }

    public int LeagueGroupId { get; set; }

    public string LeagueGroupName { get; set; }

    public int NoOfTraders { get; set; }

    public int NoOfMatches { get; set; }

    public int NoOfBetChoices { get; set; }

    public decimal Turnover { get; set; }

    public decimal WinLoss { get; set; }

    public decimal? Margin { get; set; }

    public int BetCount { get; set; }

    public decimal TurnoverReal { get; set; }

    public decimal WinLossReal { get; set; }

    public decimal? MarginReal { get; set; }

    public decimal? BetTypeBMShift1 { get; set; }

    public decimal? BetTypeBMShift2 { get; set; }

    public decimal? BetTypeBMShift3 { get; set; }

    public string Market { get; set; }

    private string GetMarketToString(Market market)
    {
        return market == SoccerPerformanceDetail.Market.Live ? "Live" : "Non Live";
    }

    public bool IsLive { get; set; }

    public int FlagForViewReport { get; set; }
}