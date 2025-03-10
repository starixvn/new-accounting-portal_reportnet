namespace SBPerformance.TopLosingLeague;

using Constants;

public class TopLosingLeagueEntity
{
    public int Id { get; set; }

    public string Site { get; set; }

    public string League { get; set; }

    public string Event { get; set; }

    public string Match { get; set; }

    public string BetType { get; set; }

    public string Username { get; set; }

    public int NoMatches { get; set; }

    public int NoBetTypes { get; set; }

    public int BetCount { get; set; }

    public int ActiveMembers { get; set; }

    public decimal RealIncome { get; set; }

    public decimal RealTurnover { get; set; }

    public decimal RealMargin => RealTurnover == 0 ? 0 : RealIncome / RealTurnover;

    public static TopLosingLeagueEntity operator +(TopLosingLeagueEntity a, TopLosingLeagueEntity b)
    {
        return new TopLosingLeagueEntity
        {
            Id = 0,
            Site = TextConstant.Total,
            League = TextConstant.Total,
            Event = TextConstant.Total,
            Match = TextConstant.Total,
            BetType = TextConstant.Total,
            Username = TextConstant.Total,
            BetCount = a.BetCount + b.BetCount,
            NoMatches = a.NoMatches + b.NoMatches,
            NoBetTypes = a.NoBetTypes + b.NoBetTypes,
            ActiveMembers = a.ActiveMembers + b.ActiveMembers,
            RealIncome = a.RealIncome + b.RealIncome,
            RealTurnover = a.RealTurnover + b.RealTurnover
        };
    }
}