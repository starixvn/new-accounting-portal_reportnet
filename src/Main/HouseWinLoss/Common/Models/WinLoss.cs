namespace HouseWinLoss.Common.Models;

public class WinLoss
{
    private const int NumberDigitOfDecimalRounding = 4;

    public string Name { get; set; }

    public int Id { get; set; }

    public decimal MemberWinLoss { get; set; }

    public decimal SMAWinLoss { get; set; }

    public decimal Commission { get; set; }

    public decimal RealIncome { get; set; }

    public decimal VirtualIncome { get; set; }

    public decimal Variance { get; set; }

    public decimal ExtraPT { get; set; }

    public decimal RealTurnover { get; set; }

    public decimal VirtualTurnover { get; set; }

    public int IsSabaSport { get; set; }

    public int SportType { get; set; }

    public decimal VirtualMargin => VirtualTurnover != 0
        ? Math.Round(VirtualIncome / VirtualTurnover, NumberDigitOfDecimalRounding, MidpointRounding.AwayFromZero)
        : 0;

    public decimal RealMargin => RealTurnover != 0
        ? Math.Round(RealIncome / RealTurnover, NumberDigitOfDecimalRounding, MidpointRounding.AwayFromZero)
        : 0;

    public bool IsEmpty
    {
        get
        {
            var isEmptyTurnover = RealTurnover == 0 && VirtualTurnover == 0;
            var isEmptyIncome = RealIncome == 0 && VirtualIncome == 0;
            var isEmptyWinLoss = MemberWinLoss == 0 && SMAWinLoss == 0;
            return isEmptyIncome && isEmptyTurnover && isEmptyWinLoss;
        }
    }
}