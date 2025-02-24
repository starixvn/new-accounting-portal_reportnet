using Fanex.Data.Repository;

namespace SoccerPerformanceDetail.Criteria;

public class LeagueExclusionConfigGetCriteria: CriteriaBase
{
    public override string GetSettingKey() => "TGRS_LeagueExclusion_SelectConfig";

    public override bool IsValid() => true;
}