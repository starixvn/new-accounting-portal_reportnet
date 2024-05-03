using Fanex.Data.Repository;
using Utilities.LinqDynamicSum;

namespace SBCustomerPerformanceByDanger.Report
{
    public interface ReportService
    {
        Task<SummaryEntity<CustomerPerformanceByDangerModel>> GetCustomerPerformanceByDanger(CustomerPerformanceByDangerQueryCriteria criteria);
    }

    public class ReportServiceImp : ReportService
    {
        private readonly IDynamicRepository repository;

        public ReportServiceImp(IDynamicRepository repository)
        {
            this.repository = repository;
        }

        public async Task<SummaryEntity<CustomerPerformanceByDangerModel>> GetCustomerPerformanceByDanger(CustomerPerformanceByDangerQueryCriteria criteria)
        {
            return (await repository.FetchAsync<CustomerPerformanceByDangerModel>(criteria)).CalculateSummaryEntity();
        }
    }
}