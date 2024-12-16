using Epplus.Mapper.Annotations;

namespace ActualMemberBet.Product;

public class ProductActualMemberBetExcel
{
    [Cell("A5")]
    public string ProductName { get; set; }

    [Cell("B5")]
    public int BetCount { get; set; }

    [Cell("C5")]
    public decimal MemberTurnoverRM { get; set; }

    [Cell("D5")]
    public decimal MemberNetWinLoss { get; set; }

    [Cell("E5")]
    public decimal HouseRealTurnover { get; set; }

    [Cell("F5")]
    public decimal HouseVirtualTurnover { get; set; }

    [Cell("G5")]
    public decimal HouseRealIncome { get; set; }

    [Cell("H5")]
    public decimal HouseVirtualWinLoss { get; set; }
}