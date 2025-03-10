namespace SoccerPerformanceDetail.Criteria;

using Fanex.Data.Repository;

public class LeagueExclusionConfigGetCriteria: CriteriaBase
{
    public override string GetSettingKey() => "TGRS_LeagueExclusion_SelectConfig";

    public override bool IsValid() => true;
}