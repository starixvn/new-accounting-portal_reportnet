namespace ActualMemberBet.Common;

using Fanex.Data.Repository;
using Utilities.LinqDynamicSum;

public interface ActualMemberBetService
{
    Task<SummaryEntity<T>> Query<T>(ICriteria criteria) where T : class, new();
}

public class ActualMemberBetServiceImp(IDynamicRepository repository) : ActualMemberBetService
{
    public async Task<SummaryEntity<T>> Query<T>(ICriteria criteria) where T : class, new()
    {
        return (await repository.FetchAsync<T>(criteria))
            .CalculateSummaryEntity();
    }
}