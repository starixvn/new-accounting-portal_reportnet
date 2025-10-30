using Fanex.Data;
using Fanex.Data.Repository;

namespace DstPerformance.Criteria;

public class ReportCriteria : CriteriaBase
{
    public string FromDate { get; set; }

    public string ToDate { get; set; }

    public bool NoDraw { get; set; }

    public int? SportId { get; set; }

    public int? BetType { get; set; }

    public int? LeagueId { get; set; }

    public byte LiveStatus { get; set; }

    [SpParam(Ignored = true)]
    public int MatchCount { get; set; }

    public override string GetSettingKey() => "Acc_Rpt_DSTPerformance";

    public override bool IsValid() => true;
}