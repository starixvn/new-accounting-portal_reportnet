using Fanex.Data.Repository;

namespace ResultAnalysis.Criteria;

public class LeagueCriteria : CriteriaBase
{
    public int SportId { get; set; }

    public string SearchText { get; set; }

    public override string GetSettingKey() => "Acc_Rpt_ResultAnalysis_LeagueSel";

    public override bool IsValid() => true;
}