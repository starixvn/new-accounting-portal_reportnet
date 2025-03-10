namespace SMAWinLoss.Filter;

using Fanex.Data.Repository;

public interface FilterService
{
    Task<IEnumerable<Currency>> GetCurrencies();

    Task<IEnumerable<Site>> GetSites();

    Task<IEnumerable<Sport>> GetSports();

    Task<LeagueGroupModel> GetLeagueGroup();

    Task<bool> GetAvailableDatabase();

    Task<DateTime> GetAvailableDateData();

    Task<IEnumerable<BetFrom>> GetBetFroms();
}

public class FilterServiceImp(IDynamicRepository repository) : FilterService
{
    public async Task<bool> GetAvailableDatabase()
    {
        var criteria = new AvailableDatabaseCriteria();
        return await repository.GetAsync<bool>(criteria);
    }

    public async Task<DateTime> GetAvailableDateData()
    {
        var criteria = new AvailableDateDataCriteria();
        return await repository.GetAsync<DateTime>(criteria);
    }

    public async Task<IEnumerable<Currency>> GetCurrencies()
    {
        var criteria = new CurrencyCriteria();
        return await repository.FetchAsync<Currency>(criteria);
    }

    public async Task<LeagueGroupModel> GetLeagueGroup()
    {
        var result = new LeagueGroupModel
        {
            NumberItems = 0,
            LeagueGroups = []
        };

        var criteria = new LeagueGroupCriteria();

        var leagueGroupChildItems = await repository.FetchAsync<LeagueGroupChildItem>(criteria);
        result.NumberItems = leagueGroupChildItems.Count();

        var leagueGroups = leagueGroupChildItems.GroupBy(x => x.Label).Select(g => g.Key);

        foreach (var key in leagueGroups)
        {
            var childItems = leagueGroupChildItems.Where(x => x.Label == key);

            var leagueGroupItem = new LeagueGroup
            {
                GroupName = key,
                LeagueIds = childItems.Select(x => x.LeagueIds).FirstOrDefault(),
                ChildItems = childItems
            };

            result.LeagueGroups.Add(leagueGroupItem);
        }


        return result;
    }

    public async Task<IEnumerable<Site>> GetSites()
    {
        var criteria = new SiteCriteria();
        return await repository.FetchAsync<Site>(criteria);
    }

    public async Task<IEnumerable<Sport>> GetSports()
    {
        var criteria = new SportCriteria();
        return await repository.FetchAsync<Sport>(criteria);
    }

    public async Task<IEnumerable<BetFrom>> GetBetFroms()
    {
        var criteria = new BetFromCriteria();
        return await repository.FetchAsync<BetFrom>(criteria);
    }
}