using Fanex.Data.Repository;
using Utilities.BalanceUpServices;

namespace SBCustomerPerformanceByDanger.Filter;

public interface FilterService
{
    Task<IEnumerable<DivisionModel>> GetDivision(DivisionQueryCriteria criteria);

    Task<IEnumerable<LeagueGroupModel>> GetLeagueGroup(LeagueGroupQueryCriteria criteria);

    Task<IEnumerable<string>> GetLeagueSuggestion(LeagueQueryCriteria criteria);

    Task<List<SportModel>> GetSport(SportQueryCriteria criteria);

    DateTime GetBalanceUpDate();
}

public class FilterServiceImp(IDynamicRepository repository, BalanceUpService balanceUpService)
    : FilterService
{
    public async Task<IEnumerable<DivisionModel>> GetDivision(DivisionQueryCriteria criteria)
    {
        return await repository.FetchAsync<DivisionModel>(criteria);
    }

    public async Task<IEnumerable<LeagueGroupModel>> GetLeagueGroup(LeagueGroupQueryCriteria criteria)
    {
        return await repository.FetchAsync<LeagueGroupModel>(criteria);
    }

    public async Task<IEnumerable<string>> GetLeagueSuggestion(LeagueQueryCriteria criteria)
    {
        return await repository.FetchAsync<string>(criteria);
    }

    public DateTime GetBalanceUpDate()
    {
        return balanceUpService.Query().BalanceUpDate;
    }

    public async Task<List<SportModel>> GetSport(SportQueryCriteria criteria)
    {
        var result = new List<SportModel>();
        var rawSports = await repository.FetchAsync<SportItemModel>(criteria);
        var sportGroups = rawSports.GroupBy(x => new { x.SportGroupType, x.SportGroupName });

        foreach(var sportGroup in sportGroups)
        {
            var sportItem = new SportModel
            {
                SportName = sportGroup.Key.SportGroupName,
                SportType = sportGroup.Key.SportGroupType,
                BettypeGroups = []
            };

            var sports = rawSports.Where(x => x.SportGroupType == sportGroup.Key.SportGroupType);
            var betTypeGroups = sports.GroupBy(x => x.BetGroupName);

            foreach(var betTypeGroup in betTypeGroups)
            {
                var betTypes = sports.Where(x => x.BetGroupName == betTypeGroup.Key);
                var betTypeGroupItem = new BettypeGroupModel
                {
                    BettypeGroupName = betTypeGroup.Key,
                    Bettypes = []
                };

                foreach(var betType in betTypes)
                {
                    betTypeGroupItem.Bettypes.Add(new BettypeModel
                    {
                        BettypeName = betType.BettypeName,
                        Bettype = $"{betType.SportType}_{betType.Bettype}"
                    });
                }

                sportItem.BettypeGroups.Add(betTypeGroupItem);
            }

            result.Add(sportItem);

        }

        return result;
    }
}