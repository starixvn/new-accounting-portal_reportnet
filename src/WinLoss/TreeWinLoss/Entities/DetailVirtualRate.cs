namespace TreeWinLoss.Entities;

public class DetailVirtualRate
{
    public int CustId { get; set; }

    public string UserName { get; set; }

    public int SportType { get; set; }

    public int BetType { get; set; }

    public bool IsLive { get; set; }

    public decimal VirtualMultiplier { get; set; }  
}