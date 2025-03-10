namespace DailyCreatedCustomers.Services;

using Criteria;
using Entities;
using Fanex.Data.Repository;
using Model;
using Utilities.DateTimes;
using Utilities.LinqDynamicSum;

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
            Summaries = []
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
        var rawData = await repository.FetchAsync<DetailEntity>(criteria);

        return rawData.Select(row => new DetailModel { CreatedDate = row.CreatedDate.ToLongFormatedDate(), UserId = row.CustId, UserName = row.UserName, Creator = row.Creator }).ToList();
    }
}