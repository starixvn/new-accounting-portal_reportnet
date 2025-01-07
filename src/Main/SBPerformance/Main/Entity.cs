using SBPerformance.Constants;

namespace SBPerformance.Main;

public class Entity
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int BetCount { get; set; }

    public decimal RealIncome { get; set; }

    public decimal RealTurnover { get; set; }

    public decimal RealMargin => RealTurnover == 0 ? 0 : RealIncome / RealTurnover;

    public static Entity operator +(Entity a, Entity b)
    {
        return new Entity
        {
            Id = 0,
            Name = TextConstant.Total,
            BetCount = a.BetCount + b.BetCount,
            RealIncome = a.RealIncome + b.RealIncome,
            RealTurnover = a.RealTurnover + b.RealTurnover
        };
    }
}