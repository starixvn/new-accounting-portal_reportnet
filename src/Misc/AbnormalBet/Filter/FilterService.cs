using AbnormalBet.Criteria;
using AbnormalBet.Model;
using Fanex.Data.Repository;

namespace AbnormalBet.Filter
{
    public interface FilterService
    {
        Task<IEnumerable<FilterItemModel>> QuerySport();

        Task<IEnumerable<FilterItemModel>> QueryReason();

        Task<IEnumerable<SiteModel>> QuerySite();

        Task<IEnumerable<FilterItemModel>> QueryBetType(string sportId);

        Task<IEnumerable<FilterItemModel>> QueryCurrency();

        Task<IEnumerable<LeagueModel>> QueryLeague(string sportId, string searchText, bool searchExactly);
    }

    public class FilterServiceImp : FilterService
    {
        private readonly IDynamicRepository repository;

        public FilterServiceImp(IDynamicRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<FilterItemModel>> QueryBetType(string sportId)
        {
            return await repository.FetchAsync<FilterItemModel>(new BetTypeQueryCriteria
            {
                SportId = sportId
            });
        }

        public async Task<IEnumerable<SiteModel>> QuerySite()
        {
            return await repository.FetchAsync<SiteModel>(new SiteQueryCriteria());
        }

        public async Task<IEnumerable<FilterItemModel>> QueryReason()
        {
            return await repository.FetchAsync<FilterItemModel>(new ReasonQueryCriteria());
        }

        public async Task<IEnumerable<FilterItemModel>> QuerySport()
        {
            return await repository.FetchAsync<FilterItemModel>(new SportQueryCriteria());
        }

        public async Task<IEnumerable<FilterItemModel>> QueryCurrency()
        {
            return await repository.FetchAsync<FilterItemModel>(new CurrencyQueryCriteria());
        }

        public async Task<IEnumerable<LeagueModel>> QueryLeague(string sportId, string searchText, bool searchExactly)
        {
            return await repository.FetchAsync<LeagueModel>(new LeagueQueryCriteria
            {
                SportId = sportId,
                SearchText = searchText,
                SearchExactly = searchExactly
            });
        }
    }
}