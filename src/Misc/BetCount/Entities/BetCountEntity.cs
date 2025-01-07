namespace BetCount.Entities;

public class BetCountEntity
{
    public string SiteName { get; set; }

    public bool IsDeposit { get; set; } 

    public int BetCount { get; set; }

    public decimal TurnOver { get; set; }   

    public decimal MemberWinLoss { get; set;}

    public decimal AgentWinLoss { get; set; }

    public decimal MasterWinLoss { get; set; }

    public decimal SuperWinLoss { get; set; }

    public decimal Commission { get; set; }

    public decimal ExtraPT { get; set; }

    public decimal CompanyWinLoss { get; set; }

    public decimal TotalWinLoss { get; set; }
}