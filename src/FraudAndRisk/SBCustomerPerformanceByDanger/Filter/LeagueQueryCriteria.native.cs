namespace SBCustomerPerformanceByDanger.Filter;

using Fanex.Data.Repository;

public class LeagueQueryCriteria : CriteriaBase
{
    public string SportBettype { get; set; }

    public string SearchText { get; set; }

    public override string GetSettingKey() => "Acc_Rpt_SBCustPerfByDangerLevel_LeagueBySportSel";

    public override bool IsValid() => true;
}