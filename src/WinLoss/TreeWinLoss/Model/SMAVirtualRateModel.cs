namespace TreeWinLoss.Model;

public class SMAVirtualRateModel
{
    public string UserName { get; set; }

    public string Sport { get; set; }

    public List<VirtualRateByBettypeModel> VirtualRateByBettypeInfo { get; set; }

    public List<SMAVirtualRateModel> ReportRows { get; set; } = [];
}