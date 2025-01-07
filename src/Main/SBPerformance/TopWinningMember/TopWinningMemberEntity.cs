using SBPerformance.Constants;

namespace SBPerformance.TopWinningMember;

public class TopWinningMemberEntity
{
    public int Id { get; set; }

    public string League { get; set; }

    public string Event { get; set; }

    public string Match { get; set; }

    public string BetType { get; set; }

    public string Sport { get; set; }

    public int NoMatches { get; set; }

    public int NoBetTypes { get; set; }

    public int BetCount { get; set; }

    public decimal RealIncome { get; set; }

    public decimal RealTurnover { get; set; }

    public decimal RealMargin => RealTurnover == 0 ? 0 : RealIncome / RealTurnover;

    public static TopWinningMemberEntity operator +(TopWinningMemberEntity a, TopWinningMemberEntity b)
    {
        return new TopWinningMemberEntity
        {
            Id = 0,
            League = TextConstant.Total,
            Event = TextConstant.Total,
            Match = TextConstant.Total,
            BetType = TextConstant.Total,
            Sport = TextConstant.Total,
            BetCount = a.BetCount + b.BetCount,
            NoMatches = a.NoMatches + b.NoMatches,
            NoBetTypes = a.NoBetTypes + b.NoBetTypes,
            RealIncome = a.RealIncome + b.RealIncome,
            RealTurnover = a.RealTurnover + b.RealTurnover
        };
    }
}