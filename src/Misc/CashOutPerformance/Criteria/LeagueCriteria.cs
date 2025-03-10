namespace CashOutPerformance.Criteria;

using Fanex.Data.Repository;

public class LeagueCriteria : CriteriaBase
{
    public string SportId { get; set; }

    public string SearchText { get; set; }

    public bool SearchExactly { get; set; }

    public override bool IsValid() => true;

    public override string GetSettingKey() => "Acc_Rpt_COPerformance_LeagueSel";
}