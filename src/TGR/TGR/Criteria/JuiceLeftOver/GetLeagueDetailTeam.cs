
using Fanex.Data.Repository;

namespace Fanex.NAP.TGRS.Reports.Criteria.PerformanceDetail;

public class GetLeagueDetailTeam : CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public short TeamId { get; set; }

    public string Bettypes { get; set; }

    public override bool IsValid() => true;

    public override string GetSettingKey() => "TGRS_Rpt_PerformanceDetail_Team_League";
}