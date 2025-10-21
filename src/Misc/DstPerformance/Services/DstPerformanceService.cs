using DstPerformance.Criteria;
using DstPerformance.Models;
using Fanex.Data.Repository;
using Utilities.LinqDynamicSum;

namespace DstPerformance.Services;

public interface DstPerformanceService
{
    Task<SportModel> GetSports(ReportCriteria criteria);
}

public class DstPerformanceServiceImp(IDynamicRepository repository) : DstPerformanceService
{
    public Task<SportModel> GetSports(ReportCriteria criteria)
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