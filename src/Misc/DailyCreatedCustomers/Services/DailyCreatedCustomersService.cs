using DailyCreatedCustomers.Criteria;
using DailyCreatedCustomers.Entities;
using DailyCreatedCustomers.Model;
using Fanex.Data.Repository;
using Utilities.DateTimes;
using Utilities.LinqDynamicSum;

namespace DailyCreatedCustomers.Services
{
    public interface DailyCreatedCustomersService
    {
        Task<SummaryModel> GetReport(DailyCreatedCustomersCriteria criteria);

        Task<List<DetailModel>> GetDetailReport(DailyCreatedCustomersDetailsCriteria criteria);
    }

    public class DailyCreatedCustomersServiceImp(IDynamicRepository repository) : DailyCreatedCustomersService
    {
        public async Task<SummaryModel> GetReport(DailyCreatedCustomersCriteria criteria)
        {
            var report = new SummaryModel
            {
                Summaries = new List<SummaryItemModel>()
            };

            var rawData = await repository.FetchAsync<SummaryEntity>(criteria);

            foreach (var row in rawData)
            {
                var item = new SummaryItemModel
                {
                    CreatedDate = row.CreatedDate.ToShortFormatedDate(),
                    SuperAmount = row.SuperAmount,
                    DirectMasterAmount = row.DirectMasterAmount,
                    DirectAgentAmount = row.DirectAgentAmount,
                    MasterAmount = row.MasterAmount,
                    AgentAmount = row.AgentAmount,
                    MemberAmount = row.MemberAmount
                };

                report.Summaries.Add(item);
            }

            var total = report.Summaries.DynamicSum();
            report.Total = total;
            
            return report;
        }

        public async Task<List<DetailModel>> GetDetailReport(DailyCreatedCustomersDetailsCriteria criteria)
        {
            var report = new List<DetailModel>();

            var rawData = await repository.FetchAsync<DetailEntity>(criteria);

            foreach (var row in rawData)
            {
                var item = new DetailModel
                {
                    CreatedDate = row.CreatedDate.ToLongFormatedDate(),
                    UserId = row.CustId,
                    UserName = row.UserName,
                    Creator = row.Creator
                };

                report.Add(item);
            }

            return report;
        }
    }
}
