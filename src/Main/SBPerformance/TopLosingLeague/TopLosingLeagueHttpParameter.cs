namespace SBPerformance.TopLosingLeague;

using MediatR;
using Utilities.LinqDynamicSum;

public class TopLosingLeagueHttpParameter : IRequest<SummaryEntity<TopLosingLeagueItemModel>>
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int TimeRange { get; set; }

    public decimal? Margin { get; set; }

    public string TopBy { get; set; }

    public bool ExcludeDrawTickets { get; set; }

    public bool ExcludeParlay { get; set; }

    public int? SiteID { get; set; }

    public int LeagueID { get; set; }

    public byte? LiveIndicator { get; set; }

    public string ShowBy { get; set; }

    public int? MatchID { get; set; }

    public int? BetType { get; set; }

    public bool IsShowFullColumn { get; set; }
}