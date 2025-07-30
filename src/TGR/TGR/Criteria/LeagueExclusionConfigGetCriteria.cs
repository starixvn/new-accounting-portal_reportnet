using Fanex.Data.Repository;

namespace TGR.Criteria;

public class LeagueExclusionConfigGetCriteria: CriteriaBase
{
    public override string GetSettingKey() => "TGRS_LeagueExclusion_SelectConfig";

    public override bool IsValid() => true;
}