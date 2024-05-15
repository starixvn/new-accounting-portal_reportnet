using Olympus.FPSAPI.Client;
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

    public class SummaryServiceImp : SummaryService
    {
        private readonly PunterAnalysisService punterAnalysisService;

        private readonly FraudServiceWrapper fraudServiceWrapper;

        private readonly ConfigurationService configurationService;


        public SummaryServiceImp(
           PunterAnalysisService punterAnalysisService,
           FraudServiceWrapper fraudServiceWrapper,
           ConfigurationService configurationService)
        {
            this.punterAnalysisService = punterAnalysisService;
            this.fraudServiceWrapper = fraudServiceWrapper;
            this.configurationService = configurationService;
        }

        public async Task<SummaryEntity<SummaryModel>> GetSummaries(Criteria criteria)
        {
            var summaries = await punterAnalysisService.Query<SummaryModel>(criteria);

            foreach (var item in summaries.Entities)
            {
                item.Category = await fraudServiceWrapper.GetCategory(configurationService.FraudSystemCategoryApi, item.CustId.ToString());
            }

            return summaries;
        }
    }
}