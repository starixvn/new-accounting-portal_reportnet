using Utilities.CustomerTrackingSystem;
using PunterAnalysis.Shared;
using PunterAnalysis.Summary.Model;
using Utilities.Configurations;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.Summary
{
    public interface SummaryService
    {
        Task<SummaryEntity<SummaryModel>> GetSummaries(Criteria criteria);
    }

    public class SummaryServiceImp(
        PunterAnalysisService punterAnalysisService,
        CustomerTrackingService customerTrackingService,
        ConfigurationService configurationService)
        : SummaryService
    {
        public async Task<SummaryEntity<SummaryModel>> GetSummaries(Criteria criteria)
        {
            var summaries = await punterAnalysisService.Query<SummaryModel>(criteria);

            foreach (var item in summaries.Entities)
            {
                item.Category = await customerTrackingService.GetCategory(configurationService.FraudSystemCategoryApi, item.CustId.ToString());
            }

            return summaries;
        }
    }
}