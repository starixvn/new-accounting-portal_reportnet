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
        var data = new ReportModel
        {
            Items = rawData,
            Total = rawData.DynamicSum()
        };
        return Task.FromResult(data);
    }
}