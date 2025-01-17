using Fanex.Data;
using Fanex.Data.Repository;

namespace BetConstructPerformance.Criteria;

public class SportCriteria : CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public bool NoDraw { get; set; }

    public int? SportId { get; set; }

    public byte? LiveStatus { get; set; }

    [SpParam(Ignored = true)]
    public int MatchCount { get; set; }

    public override string GetSettingKey() => "Acc_Rpt_BetConstructPerformance";

    public override bool IsValid() => true;
}