using Fanex.Data.Repository;

namespace ResultAnalysis.Criteria;

public class ResultAnalysisCriteria: CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public int SportId { get; set; }

    public string LeagueGroupIds { get; set; }

    public string LeagueName { get; set; }

    public override string GetSettingKey() => "Acc_Rpt_ResultAnalysis";

    public override bool IsValid() => true;
}