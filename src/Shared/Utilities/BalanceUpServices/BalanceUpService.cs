using Fanex.Caching;
using Fanex.Data.Repository;

namespace Utilities.BalanceUpServices
{
    public interface BalanceUpService
    {
        BalanceUpRange Query();
    }

    public class BalanceUpServiceImp : BalanceUpService
    {
        private const string BalanceUpRangeCacheKey = "CommonBalanceUpRangeCacheKey";

        private readonly IDynamicRepository repository;

        private readonly ICacheService cacheService;

        public BalanceUpServiceImp(IDynamicRepository repository, ICacheService cacheService)
        {
            this.repository = repository;
            this.cacheService = cacheService;
        }

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
}
