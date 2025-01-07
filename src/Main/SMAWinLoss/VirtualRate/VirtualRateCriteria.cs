namespace SMAWinLoss.VirtualRate;

using Fanex.Data.Repository;

public class VirtualRateCriteria : CriteriaBase
{
    public int CustId { get; set; }

    public override string GetSettingKey() => "SMAWinLoss-VirtualRate";

    public override bool IsValid() => true;
}