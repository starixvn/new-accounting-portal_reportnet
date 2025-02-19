namespace SoccerPerformanceDetail.ViewModel;

using SoccerPerformanceDetail.Model;

public class LeaguePerformanceItem
{
    public LeaguePerformanceItem()
    {
    }

    public LeaguePerformanceItem(LeaguePerformanceInfo leagueData)
    {
        if (leagueData != null && leagueData.LeagueId.HasValue)
        {
            this.LeagueId = leagueData.LeagueId.Value;
            this.LeagueName = leagueData.LeagueName;
            this.LeagueGroupId = Convert.ToInt32(leagueData.LeagueGroupID);
            this.NoOfTraders = Convert.ToInt32(leagueData.TraderCount);
            this.NoOfMatches = Convert.ToInt32(leagueData.MatchCount);
            this.NoOfBetChoices = Convert.ToInt32(leagueData.BetChoiceCount);
            this.Turnover = Convert.ToDecimal(leagueData.Turnover);
            this.WinLoss = Convert.ToDecimal(leagueData.WinLoss);
            this.Margin = leagueData.Margin;
            this.TurnoverReal = Convert.ToDecimal(leagueData.TurnoverReal);
            this.WinLossReal = Convert.ToDecimal(leagueData.WinLossReal);
            this.MarginReal = leagueData.MarginReal;
            this.BetCount = Convert.ToInt32(leagueData.BetCount);
            this.BetTypeBMShift1 = leagueData.BetTypeBMShift1;
            this.BetTypeBMShift2 = leagueData.BetTypeBMShift2;
            this.BetTypeBMShift3 = leagueData.BetTypeBMShift3;              
            this.Market = GetMaketToString(leagueData.Market);
            this.IsLive = leagueData.IsLive;
            this.FlagForViewReport = leagueData.FlagForViewReport;
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

    private string GetMaketToString(Market market)
    {
        if (market == SoccerPerformanceDetail.Market.Live)
        {
            return "Live";
        }

        return "Non Live";
    }

    public bool IsLive { get; set; }

    public int FlagForViewReport { get; set; }
}