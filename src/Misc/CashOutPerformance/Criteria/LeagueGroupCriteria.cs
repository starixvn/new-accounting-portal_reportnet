using Fanex.Data.Repository;

namespace CashOutPerformance.Criteria;

public class LeagueGroupCriteria : CriteriaBase
{
    public override string GetSettingKey() => "Acc_Rpt_COPerformance_LeagueGroupSel";

    public override bool IsValid() => true;
}