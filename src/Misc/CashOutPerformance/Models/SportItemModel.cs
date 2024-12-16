namespace CashOutPerformance.Models;

public class SportItemModel : CommonTicketModel
{
    public int SportType { get; set; }

    public string SportName { get; set; }

    public string LeagueName { get; set; }

    public static SportItemModel operator +(SportItemModel a, SportItemModel b)
    {
        return new SportItemModel
        {
            SportType = a.SportType,
            SportName = a.SportName,
            LeagueName = string.Empty,
            COTurnover_RM = a.COTurnover_RM + b.COTurnover_RM,
            BBAmount_RM = a.BBAmount_RM + b.BBAmount_RM,
            MemberWL_RM = a.MemberWL_RM + b.MemberWL_RM,
            Comm_RM = a.Comm_RM + b.Comm_RM,
            COIncome_RM = a.COIncome_RM + b.COIncome_RM,
            TotalIncome_RM = a.TotalIncome_RM + b.TotalIncome_RM,
            ActualTurnover = a.ActualTurnover + b.ActualTurnover,
            AssumedPayout = a.AssumedPayout + b.AssumedPayout,
            HypGGR = a.HypGGR + b.HypGGR,
            COProfit = a.COProfit + b.COProfit
        };
    }
}