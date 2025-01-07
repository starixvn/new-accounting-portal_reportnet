namespace TreeWinLoss.Entities;

public class SummaryVirtualRate
{
    public decimal ActualRate { get; set; }

    public decimal EnableVirtualRate { get; set; }

    public decimal MinVirtualMultiplier{ get; set; }

    public decimal MaxVirtualMultiplier { get; set; }

    public bool VirtualRateLockedStatusSoccer{ get; set; }

    public bool VirtualRateLockedStatusOtherSports { get; set; }
}