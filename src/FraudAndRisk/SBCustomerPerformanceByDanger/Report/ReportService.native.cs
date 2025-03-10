namespace SBCustomerPerformanceByDanger.Report;

using Fanex.Data.Repository;
using Utilities.LinqDynamicSum;

public interface ReportService
{
    Task<SummaryEntity<CustomerPerformanceByDangerModel>> GetCustomerPerformanceByDanger(CustomerPerformanceByDangerQueryCriteria criteria);
}

public class ReportServiceImp(IDynamicRepository repository) : ReportService
{
    public async Task<SummaryEntity<CustomerPerformanceByDangerModel>> GetCustomerPerformanceByDanger(CustomerPerformanceByDangerQueryCriteria criteria)
    {
        return (await repository.FetchAsync<CustomerPerformanceByDangerModel>(criteria)).CalculateSummaryEntity();
    }
}