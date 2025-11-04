using DstPerformance.Criteria;
using DstPerformance.Models;
using Fanex.Data.Repository;
using Utilities.LinqDynamicSum;

namespace DstPerformance.Services;

public interface DstPerformanceService
{
    Task<ReportModel> GetData(ReportCriteria criteria);
}

public class DstPerformanceServiceImp(IDynamicRepository repository) : DstPerformanceService
{
    public Task<ReportModel> GetData(ReportCriteria criteria)
    {
        var rawData = repository.Fetch<ReportItemModel>(criteria);
        var parlayData = rawData.Where(r => r.SportId.IsParlay());
        var data = new ReportModel
        {
            Items = rawData,
            Total = rawData.DynamicSum()
        };
        if (data.Items.All(i => i.SportId.IsParlay()))
        {
            data.Total.SportId = -1;
        }
        else
        {
            data.Total.MatchCount -= parlayData.Sum(s => s.MatchCount);
        }
        if (criteria is { SportId: not null, BetType: null })
        {
            data.Total.MatchCount = criteria.MatchCount;
        }
        return Task.FromResult(data);
    }
}