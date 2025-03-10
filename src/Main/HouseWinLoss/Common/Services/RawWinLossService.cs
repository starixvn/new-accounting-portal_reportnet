namespace HouseWinLoss.Common.Services;

using Criteria;
using Fanex.Data.Repository;
using Models;

public interface RawWinLossService
{
    Task<List<WinLossRaw>> Query(WinLossCriteria criteria);
}

public class RawWinLossServiceImp(IDynamicRepository repository) : RawWinLossService
{
    public virtual async Task<List<WinLossRaw>> Query(WinLossCriteria criteria)
    {
        return (await repository.FetchAsync<WinLossRaw>(criteria)).ToList();
    }
}