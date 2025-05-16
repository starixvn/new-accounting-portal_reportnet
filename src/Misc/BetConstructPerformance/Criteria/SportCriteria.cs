namespace BetConstructPerformance.Criteria;

using Fanex.Data;
using Fanex.Data.Repository;

public class SportCriteria : CriteriaBase
{
    public string FromDate { get; set; }

    public string ToDate { get; set; }

    public bool NoDraw { get; set; }

    public int? SportId { get; set; }

    public byte? LiveStatus { get; set; }

    public string FilterType { get; set; }

    [SpParam(Ignored = true)]
    public int MatchCount { get; set; }

    public override string GetSettingKey() => "Acc_Rpt_BetConstructPerformance";

    public override bool IsValid() => true;
}