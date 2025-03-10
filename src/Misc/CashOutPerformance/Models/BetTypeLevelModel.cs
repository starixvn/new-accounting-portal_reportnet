namespace CashOutPerformance.Models;

public class BetTypeLevelModel : CommonTicketModel
{
    public int BetType { get; set; }

    public string BetTypeName { get; set; }

    public string OutCome { get; set; }

    public string WinningProbabilityQuantile { get; set; }

    public string GameTime { get; set; }

    public int BetCount { get; set; }

    public List<ChildItemBetTypeModel> ChildItems { get; set; } = [];

    public static BetTypeLevelModel operator +(BetTypeLevelModel a, BetTypeLevelModel b)
    {
        return new BetTypeLevelModel
        {
            BetType = a.BetType,
            BetTypeName = a.BetTypeName,
            OutCome = a.OutCome,
            GameTime = a.GameTime,
            WinningProbabilityQuantile = a.WinningProbabilityQuantile,
            BetCount = a.BetCount + b.BetCount,
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

public class ChildItemBetTypeModel : CommonTicketModel
{
    public int BetType { get; set; }

    public string BetTypeName { get; set; }

    public string OutCome { get; set; }

    public string WinningProbabilityQuantile { get; set; }

    public string GameTime { get; set; }

    public int BetCount { get; set; }
}