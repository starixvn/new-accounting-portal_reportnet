using Fanex.Data.Repository;
using TGR.Criteria.SoccerPerformanceDetail;
using TGR.Model.SoccerPerformanceDetail;

namespace TGR.Services;

public interface DataService
{
    Task<LeagueExclusionConfig> GetLeagueExclusionConfig();
}

public class DataServiceImp(IDynamicRepository repository) : DataService
{
    public async Task<LeagueExclusionConfig> GetLeagueExclusionConfig()
    {
        return await repository.GetAsync<LeagueExclusionConfig>(new LeagueExclusionConfigGetCriteria());
    }
}