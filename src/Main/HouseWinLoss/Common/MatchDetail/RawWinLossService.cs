namespace HouseWinLoss.Common.MatchDetail;

using Criteria;
using Fanex.Data.Repository;

public interface RawWinLossService
{
    Task<List<RawWinLoss>> Query(WinLossCriteria criteria);
}

public class RawWinLossServiceImp(IDynamicRepository repository) : RawWinLossService
{
    public virtual async Task<List<RawWinLoss>> Query(WinLossCriteria criteria)
    {
        return (await repository.FetchAsync<RawWinLoss>(criteria)).ToList();
    }
}