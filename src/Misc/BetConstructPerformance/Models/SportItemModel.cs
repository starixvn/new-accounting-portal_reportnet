namespace BetConstructPerformance.Models;

public class SportItemModel
{
    public int SportId { get; set; }

    public string Sport { get; set; }

    public DateTime Date { get; set; }

    public string LeagueName { get; set; }

    public string MatchName { get; set; }

    public string BetTypeName { get; set; }

    public string LiveStatus { get; set; }

    public int MatchCount { get; set; }

    public int BetCount { get; set; }

    public decimal MemberWinLoss { get; set; }

    public decimal MemberTurnover { get; set; }

    public decimal HouseIncome { get; set; }

    public decimal HouseTurnover { get; set; }

    public decimal AmountDueToBC { get; set; }

    public decimal? Margin
    {
        get
        {
            if (HouseTurnover == 0)
            {
                return 0;
            }

            return HouseIncome / HouseTurnover;
        }
    }

    public static SportItemModel operator +(SportItemModel a, SportItemModel b)
    {
        return new SportItemModel
        {
            Sport = "Grand Total",
            MatchCount = a.MatchCount + b.MatchCount,
            BetCount = a.BetCount + b.BetCount,
            MemberWinLoss = a.MemberWinLoss + b.MemberWinLoss,
            MemberTurnover = a.MemberTurnover + b.MemberTurnover,
            HouseIncome = a.HouseIncome + b.HouseIncome,
            HouseTurnover = a.HouseTurnover + b.HouseTurnover,
            AmountDueToBC = a.AmountDueToBC + b.AmountDueToBC
        };
    }
}