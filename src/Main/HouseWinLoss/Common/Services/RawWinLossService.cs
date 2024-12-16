using Fanex.Data.Repository;
using HouseWinLoss.Common.Criteria;
using HouseWinLoss.Common.Models;

namespace HouseWinLoss.Common.Services;

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