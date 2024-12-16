namespace ActualMemberBet.Bettype;

public class BetTypeActualMemberBet
{
    public string BettypeName { get; set; }

    public int BetCount { get; set; }

    public decimal MemberTurnoverRM { get; set; }

    public decimal MemberTurnoverMYOdds { get; set; }


    public decimal MemberNetWinLoss { get; set; }

    public decimal HouseRealTurnoverMYOdds { get; set; }

    public decimal HouseVirtualTurnoverMyOdds { get; set; }

    public decimal HouseRealIncome { get; set; }

    public decimal HouseVirtualWinLoss { get; set; }

    public static BetTypeActualMemberBet operator +(BetTypeActualMemberBet a, BetTypeActualMemberBet b)
    {
        return new BetTypeActualMemberBet
        {
            BettypeName = "Grand Total",
            BetCount = a.BetCount + b.BetCount,
            MemberTurnoverRM = a.MemberTurnoverRM + b.MemberTurnoverRM,
            MemberTurnoverMYOdds = a.MemberTurnoverMYOdds + b.MemberTurnoverMYOdds,
            MemberNetWinLoss = a.MemberNetWinLoss + b.MemberNetWinLoss,
            HouseRealTurnoverMYOdds = a.HouseRealTurnoverMYOdds + b.HouseRealTurnoverMYOdds,
            HouseVirtualTurnoverMyOdds = a.HouseVirtualTurnoverMyOdds + b.HouseVirtualTurnoverMyOdds,
            HouseRealIncome = a.HouseRealIncome + b.HouseRealIncome,
            HouseVirtualWinLoss = a.HouseVirtualWinLoss + b.HouseVirtualWinLoss
        };
    }
}