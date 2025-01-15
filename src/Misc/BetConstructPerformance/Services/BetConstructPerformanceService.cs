using BetConstructPerformance.Criteria;
using BetConstructPerformance.Models;
using Fanex.Data.Repository;
using Utilities.LinqDynamicSum;

namespace BetConstructPerformance.Services;

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
            data.Total.MatchCount = rawData.Select(x => x.MatchId).Distinct().Count();
        }
        return Task.FromResult(data);
    }
}