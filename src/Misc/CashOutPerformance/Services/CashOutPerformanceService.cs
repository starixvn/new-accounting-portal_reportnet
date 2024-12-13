using CashOutPerformance.Criteria;
using CashOutPerformance.Models;
using CashOutPerformance.ViewModels;
using Fanex.Data.Repository;
using Utilities.LinqDynamicSum;

namespace CashOutPerformance.Services
{
    public interface CashOutPerformanceService
    {
        Task<IEnumerable<FilterGroupItemModel>> GetSportBettypes(SportCriteria criteria);

        Task<IEnumerable<SiteModel>> GetLicensees(LicenseeCriteria criteria);

        Task<TicketViewModel<T>> GetTickets<T, U>(U criteria);

        Task<IEnumerable<LeagueModel>> GetLeagues(LeagueCriteria criteria);

        Task<IEnumerable<FilterItemModel>> GetLeagueGroups(LeagueGroupCriteria criteria);

        Task<SportLevelModel> GetSportLevel(SportLevelCriteria criteria);
    }

    public class CashOutPerformanceServiceImp(IDynamicRepository repository) : CashOutPerformanceService
    {
        public async Task<IEnumerable<SiteModel>> GetLicensees(LicenseeCriteria criteria)
        {
            return await repository.FetchAsync<SiteModel>(criteria);
        }

        public async Task<IEnumerable<FilterGroupItemModel>> GetSportBettypes(SportCriteria criteria)
        {
            var sportBettype = await repository.FetchAsync<SportBetTypeModel>(criteria);
            var groups = sportBettype.GroupBy(x => new { x.SportType, x.SportName });
            return groups.Select(group => new FilterGroupItemModel { GroupId = group.Key.SportType, GroupName = group.Key.SportName, ChildItems = group.Select(x => new FilterItemModel { Id = x.Bettype, Name = x.BettypeName }).ToList() }).ToList();
        }

        public async Task<TicketViewModel<T>> GetTickets<T, U>(U criteria)
        {
            var (firstResultSet, secondResultSet, thirdResultSet) = await repository.FetchMultipleAsync<T, T, int>((ICriteria)criteria);
            return new TicketViewModel<T>
            {
                TicketList = firstResultSet,
                TotalTicket = secondResultSet.FirstOrDefault(),
                Total = thirdResultSet.FirstOrDefault()
            };
        }

        public async Task<IEnumerable<LeagueModel>> GetLeagues(LeagueCriteria criteria)
        {
            return await repository.FetchAsync<LeagueModel>(criteria);
        }

        public async Task<IEnumerable<FilterItemModel>> GetLeagueGroups(LeagueGroupCriteria criteria)
        {
            return await repository.FetchAsync<FilterItemModel>(criteria);
        }

        public async Task<SportLevelModel> GetSportLevel(SportLevelCriteria criteria)
        {
            var (firstResultSet, secondResultSet, thirdResultSet) = await repository.FetchMultipleAsync<SportItemModel, SportItemModel, int>(criteria);

            var groups = firstResultSet.Select(r => new { r.SportName, r.SportType })
                            .Distinct();
            var sports = from @group in groups
                      let rawSports = firstResultSet.Where(r => r.SportType == @group.SportType && r.SportName == @group.SportName).ToArray()
                      select CalculateSport(rawSports);

            return new SportLevelModel
            {
                Sports = sports,
                GrandTotal = secondResultSet.FirstOrDefault(),
                TotalItems = thirdResultSet.FirstOrDefault()
            };
        }

        private SportModel CalculateSport(IEnumerable<SportItemModel> rawData)
        {
            return new SportModel
            {
                SportTotal = rawData.DynamicSum(),
                ChildItems = rawData
            };
        }
    }
}