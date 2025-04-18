namespace Utilities.BalanceUpServices;

using Fanex.Caching;
using Fanex.Data.Repository;

public interface BalanceUpService
{
    BalanceUpRange Query();
}

public class BalanceUpServiceImp(IDynamicRepository repository, ICacheService cacheService) : BalanceUpService
{
    private const string BalanceUpRangeCacheKey = "CommonBalanceUpRangeCacheKey";

    public BalanceUpRange Query()
    {
        return cacheService.GetOrSet(
            BalanceUpRangeCacheKey,
            QueryFromSource);
    }

    public BalanceUpRange QueryFromSource()
    {
        var criteria = new BalanceUpCriteria();
        return repository.Get<BalanceUpRange>(criteria);
    }
}