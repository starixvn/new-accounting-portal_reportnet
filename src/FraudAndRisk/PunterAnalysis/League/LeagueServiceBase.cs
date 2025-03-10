namespace PunterAnalysis.League;

using Fanex.Data.Repository;
using Model;
using Utilities.LinqDynamicSum;

public abstract class LeagueServiceBase(IDynamicRepository repository) : LeagueService
{
    public async Task<ReportModel> Query(ICriteria criteria)
    {
        var entities = await QueryFromRepository(criteria);
        var monthGroups = GetMappingGroups(entities);

        var leagueModels = GetLeagueModels(entities, monthGroups);

        return new ReportModel
        {
            Entities = leagueModels,
            GroupNames = GetGroupNames(monthGroups).ToList(),
            Total = leagueModels.DynamicSum()
        };
    }

    private async Task<List<LeagueMappingModel>> QueryFromRepository(ICriteria criteria)
    {
        var entities = await repository.FetchAsync<LeagueRawModel>(criteria);
        return entities
            .Select(Transform)
            .ToList();
    }

    protected abstract LeagueMappingModel Transform(LeagueRawModel rawModel);

    public static Dictionary<int, List<int>> GetMappingGroups(IEnumerable<LeagueMappingModel> entities)
    {
        return entities.Select(e => new { e.Year, e.MonthOrWeek })
            .GroupBy(e => e.Year)
            .OrderBy(g => g.Key)
            .ToDictionary(
                e => e.Key,
                e => e.Select(x => x.MonthOrWeek).Distinct().OrderBy(m => m).ToList());
    }

    private List<LeagueModel> GetLeagueModels(IEnumerable<LeagueMappingModel> entities, Dictionary<int, List<int>> monthGroups)
    {
        var entitiesByLeagueGroups = entities.GroupBy(e => new { e.LeagueId, e.LeagueName });

        return entitiesByLeagueGroups
            .Select(group => new LeagueModel
            {
                LeagueName = group.Key.LeagueName,
                PeriodGroups = GetPeriodGroupOfLeague(group, monthGroups).ToList()
            })
            .ToList();
    }

    private IEnumerable<PeriodGroup> GetPeriodGroupOfLeague(IEnumerable<LeagueMappingModel> entitiesByLeague, IDictionary<int, List<int>> monthGroups)
    {
        foreach (var year in monthGroups.Keys)
        {
            var entitiesByYear = entitiesByLeague.Where(e => e.Year == year).ToArray();
            yield return GetPeriodGroupBySumOf(entitiesByYear);

            foreach (var entitiesByMonth in monthGroups[year].Select(month => entitiesByYear.Where(e => e.MonthOrWeek == month).ToArray()))
            {
                yield return GetPeriodGroupBySumOf(entitiesByMonth);
            }
        }

        yield return GetPeriodGroupBySumOf(entitiesByLeague);
    }

    private PeriodGroup GetPeriodGroupBySumOf(IEnumerable<LeagueMappingModel> entities)
    {
        return new PeriodGroup
        {
            BetCount = entities.Sum(e => e.BetCount),
            WinLoss = entities.Sum(e => e.WinLoss),
            Turnover = entities.Sum(e => e.Turnover)
        };
    }

    public abstract IEnumerable<string> GetGroupNames(IDictionary<int, List<int>> indexGroups);
}