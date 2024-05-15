using Fanex.Data.Repository;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.Shared
{
    public interface PunterAnalysisService
    {
        Task<SummaryEntity<T>> Query<T>(ICriteria criteria) where T : class, new();
    }

    public class PunterAnalysisServiceImp : PunterAnalysisService
    {
        private readonly IDynamicRepository repository;

        public PunterAnalysisServiceImp(IDynamicRepository repository)
        {
            this.repository = repository;
        }

        public async Task<SummaryEntity<T>> Query<T>(ICriteria criteria) where T : class, new()
        {
            var entities = await repository.FetchAsync<T>(criteria);
            return entities.CalculateSummaryEntity();
        }
    }
}