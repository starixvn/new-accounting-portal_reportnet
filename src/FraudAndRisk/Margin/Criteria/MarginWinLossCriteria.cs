namespace Margin.Criteria;

using Fanex.Data;
using Fanex.Data.Repository;

public class MarginWinLossCriteria : CriteriaBase
{
    [SpParam(SqlParamName = "actual_stake")]
    public decimal ActualStake { get; set; }

    public override string GetSettingKey() => "Special_MarginReport_BetRatioByUser";

    public override bool IsValid() => true;
}