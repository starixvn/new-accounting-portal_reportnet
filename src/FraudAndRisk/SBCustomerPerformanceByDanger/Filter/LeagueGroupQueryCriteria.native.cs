namespace SBCustomerPerformanceByDanger.Filter;

using Fanex.Data.Repository;

public class LeagueGroupQueryCriteria : CriteriaBase
{
    public string SportBettype { get; set; }

    public override string GetSettingKey() => "Acc_Rpt_SBCustPerfByDangerLevel_LeagueGroupBySportSel";

    public override bool IsValid() => true;
}