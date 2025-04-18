namespace PunterAnalysis.Shared;

using Fanex.Data.Repository;
using Utilities.LinqDynamicSum;

public interface PunterAnalysisService
{
    Task<SummaryEntity<T>> Query<T>(ICriteria criteria) where T : class, new();
}

public class PunterAnalysisServiceImp(IDynamicRepository repository) : PunterAnalysisService
{
    public async Task<SummaryEntity<T>> Query<T>(ICriteria criteria) where T : class, new()
    {
        var entities = await repository.FetchAsync<T>(criteria);
        return entities.CalculateSummaryEntity();
    }
}