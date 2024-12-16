using Fanex.Caching;
using Fanex.Data.Repository;

namespace Utilities.Currencies;

public interface CurrenciesService
{
    Task<IEnumerable<CurrenciesModel>> GetCurrencies();
}

public class CurrenciesServiceImp(IDynamicRepository repository, ICacheService cacheService) : CurrenciesService
{
    private const string CurrenciesCacheKey = "CurrenciesCacheKey";

    public async Task<IEnumerable<CurrenciesModel>> GetCurrencies()
    {
        return await cacheService.GetOrSetAsync(
            CurrenciesCacheKey,
            QueryCurrenciesFromSource);
    }

    private async Task<IEnumerable<CurrenciesModel>> QueryCurrenciesFromSource()
    {
        var criteria = new CurrenciesCriteria();

        var result = await repository.FetchAsync<CurrenciesModel>(criteria);

        return result;
    }
}