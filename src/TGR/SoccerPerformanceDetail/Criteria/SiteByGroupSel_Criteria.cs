using Fanex.Data.Repository;

namespace SoccerPerformanceDetail.Criteria;
public class SiteByGroupSel_Criteria : CriteriaBase
{
    public override string GetSettingKey() => "RMS_SiteByGroupSel";

    public override bool IsValid() => true;
}