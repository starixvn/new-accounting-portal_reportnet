
namespace SoccerPerformanceDetail.Criteria
{
    using Fanex.Data.Repository;

    public class GetLeagueGroupSoccerCriteria : CriteriaBase
    {
        public string LeagueGroupIds { get; set; }

        public override string GetSettingKey()
        {
            return "TGRS_LeagueGroupSoccer_Get";
        }

        public override bool IsValid()
        {
            return true;
        }

    }
}
