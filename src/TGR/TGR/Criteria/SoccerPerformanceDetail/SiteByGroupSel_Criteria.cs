using Fanex.Data.Repository;

namespace TGR.Criteria.SoccerPerformanceDetail;
public class SiteByGroupSel_Criteria : CriteriaBase
{
    public override string GetSettingKey() => "RMS_SiteByGroupSel";

    public override bool IsValid() => true;
}