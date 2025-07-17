namespace LeagueWinLoss.Criteria;

using Fanex.Data.Repository;

public class LeagueGroupCriteria : CriteriaBase
{
    public override string GetSettingKey() => "Acc_Rpt_LeagueWL_LeagueGroupSel";

    public override bool IsValid() => true;
}