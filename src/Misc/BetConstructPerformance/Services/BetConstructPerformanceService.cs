namespace BetConstructPerformance.Services;

using Criteria;
using Fanex.Data.Repository;
using Models;
using Utilities.LinqDynamicSum;

public interface BetConstructPerformanceService
{
    Task<SportModel> GetSports(SportCriteria criteria);
}

public class BetConstructPerformanceServiceImp(IDynamicRepository repository) : BetConstructPerformanceService
{
    public Task<SportModel> GetSports(SportCriteria criteria)
    {
        var rawData = repository.Fetch<SportItemModel>(criteria);
        var data = new SportModel
        {
            Items = rawData,
            Total = rawData.DynamicSum()
        };
        if (criteria.SportId.HasValue)
        {
            data.Total.MatchCount = criteria.MatchCount;
        }
        return Task.FromResult(data);
    }
}