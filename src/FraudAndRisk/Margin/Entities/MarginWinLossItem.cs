namespace Margin.Entities;

public class MarginWinLossItem
{
    public string UserName { get; set; }

    public int BettypeId { get; set; }

    public decimal WinningAmount { get; set; }

    public decimal Turnover { get; set; }

    public decimal Ratio { get; set; }

    public int TotalBet { get; set; }
}