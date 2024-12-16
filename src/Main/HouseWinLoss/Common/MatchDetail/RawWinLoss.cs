namespace HouseWinLoss.Common.MatchDetail;

public class RawWinLoss
{
    public int MatchId { get; set; }

    public string MatchCode { get; set; }

    public DateTime EventDate { get; set; }

    public int SportType { get; set; }

    public bool IsNeutral { get; set; }

    public int HomeId { get; set; }

    public int AwayId { get; set; }

    public string HomeName { get; set; }

    public string AwayName { get; set; }

    public int LeagueId { get; set; }

    public string LeagueCode { get; set; }

    public decimal MemberWinLoss { get; set; }

    public decimal SMAWinLoss { get; set; }

    public decimal Commission { get; set; }

    public decimal RealIncome { get; set; }

    public decimal VirtualIncome { get; set; }

    public decimal Variance { get; set; }

    public decimal RealTurnover { get; set; }

    public decimal VirtualTurnover { get; set; }

    public decimal ExtraPT { get; set; }

    public static RawWinLoss operator +(RawWinLoss a, RawWinLoss b)
    {
        return new RawWinLoss
        {
            AwayId = 0,
            AwayName = string.Empty,
            HomeId = 0,
            HomeName = string.Empty,
            IsNeutral = false,
            LeagueId = 0,
            LeagueCode = string.Empty,
            MatchCode = string.Empty,
            MatchId = 0,
            EventDate = DateTime.MinValue,
            SportType = 0,
            SMAWinLoss = a.SMAWinLoss + b.SMAWinLoss,
            ExtraPT = a.ExtraPT + b.ExtraPT,
            MemberWinLoss = a.MemberWinLoss + b.MemberWinLoss,
            Commission = a.Commission + b.Commission,
            RealIncome = a.RealIncome + b.RealIncome,
            VirtualIncome = a.VirtualIncome + b.VirtualIncome,
            RealTurnover = a.RealTurnover + b.RealTurnover,
            VirtualTurnover = a.VirtualTurnover + b.VirtualTurnover,
            Variance = a.Variance + b.Variance
        };
    }
}