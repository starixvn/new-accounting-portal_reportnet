namespace DstPerformance.Models;

public class ReportItemModel
{
    public string Level { get; set; }

    public int SportId { get; set; }

    public string Sport { get; set; }

    public DateTime Date { get; set; }

    public int LeagueId { get; set; }

    public string LeagueName { get; set; }

    public string MatchName { get; set; }

    public int MatchId { get; set; }

    public int BetType { get; set; }

    public string BetTypeName { get; set; }

    public int MatchCount { get; set; }

    public int BetCount { get; set; }

    public decimal MemberWinLoss { get; set; }

    public decimal MemberTurnover { get; set; }

    public decimal HouseIncome { get; set; }

    public decimal HouseTurnover { get; set; }

    public decimal AmountDueToDST { get; set; }

    public decimal ActualTurnover { get; set; }

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

    public static ReportItemModel operator +(ReportItemModel a, ReportItemModel b)
    {
        return new ReportItemModel
        {
            Sport = "Grand Total",
            MatchCount = a.MatchCount + b.MatchCount,
            BetCount = a.BetCount + b.BetCount,
            MemberWinLoss = a.MemberWinLoss + b.MemberWinLoss,
            MemberTurnover = a.MemberTurnover + b.MemberTurnover,
            HouseIncome = a.HouseIncome + b.HouseIncome,
            HouseTurnover = a.HouseTurnover + b.HouseTurnover,
            AmountDueToDST = a.AmountDueToDST + b.AmountDueToDST,
            ActualTurnover = a.ActualTurnover + b.ActualTurnover
        };
    }
}