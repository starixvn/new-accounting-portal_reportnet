using Epplus.Mapper.Annotations;

namespace ActualMemberBet.Bettype;

public class BetTypeActualMemberBetExcel
{
    [Cell("A5")]
    public string BettypeName { get; set; }

    [Cell("B5")]
    public int BetCount { get; set; }

    [Cell("C5")]
    public decimal MemberTurnoverRM { get; set; }

    [Cell("D5")]
    public decimal MemberTurnoverMYOdds { get; set; }

    [Cell("E5")]
    public decimal MemberNetWinLoss { get; set; }

    [Cell("F5")]
    public decimal HouseRealTurnoverMYOdds { get; set; }

    [Cell("G5")]
    public decimal HouseVirtualTurnoverMyOdds { get; set; }

    [Cell("H5")]
    public decimal HouseRealIncome { get; set; }

    [Cell("I5")]
    public decimal HouseVirtualWinLoss { get; set; }
}