namespace TGR.Criteria;

public interface IBasePerformanceCriteria
{
    Market MarketFilter { get; set; }
    DateTime FromDate { get; set; }
    DateTime ToDate { get; set; }
    string BetTypes { get; set; }
    string LeagueGroupIds { get; set; }

    string VrSettings { get; set; }

    string MatchGroup { get; set; }
}