namespace PunterAnalysis.Summary;

using Model;
using Shared;
using Utilities.Configurations;
using Utilities.CustomerTrackingSystem;
using Utilities.LinqDynamicSum;

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