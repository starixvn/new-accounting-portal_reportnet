using Fanex.Data.Repository;
using Utilities.CustomerTrackingSystem;
using PunterAnalysis.AssociatedAccount.Model;
using Utilities.Configurations;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.AssociatedAccount
{
    public interface AssociatedFacade
    {
        Task<SummaryEntity<AssociatedSummaryModel>> Query(HttpParameterJson request);
    }

    public class AssociatedFacadeImp(
        IDynamicRepository repository,
        CustomerTrackingService customerTrackingService,
        ConfigurationService configurationService)
        : AssociatedFacade
    {
        public async Task<SummaryEntity<AssociatedSummaryModel>> Query(HttpParameterJson request)
        {
            var summaries = await GetMemberSummaries(request);

            return summaries
                .CalculateSummaryEntity();
        }

        private async Task<List<AssociatedSummaryModel>> GetMemberSummaries(HttpParameterJson request)
        {
            var criteria = new MemberPerformanceCriteria
            {
                CustIds = request.CustId.ToString(),
                FromDate = request.FromDate,
                ToDate = request.ToDate,
                LeagueId = request.LeagueId,
                ListCombinationId = request.ListCombinationId,
                OrderBy = request.OrderBy,
                PeriodType = request.PeriodType,
                SortBy = request.SortBy,
                ListLeagueGroupId = request.ListLeagueGroupId
            };

            var summaries = await repository.FetchAsync<AssociatedSummaryModel>(criteria);

            foreach (var item in summaries)
            {
                item.Category = await customerTrackingService.GetCategory(configurationService.FraudSystemCategoryApi, item.CustId.ToString());
            }

            return summaries.ToList();
        }
    }
}