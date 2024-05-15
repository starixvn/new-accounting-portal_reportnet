using Fanex.Data.Repository;
using PunterAnalysis.League.Model;
using Utilities.LinqDynamicSum;

namespace PunterAnalysis.League
{
    public abstract class LeagueServiceBase : LeagueService
    {
        private readonly IDynamicRepository repository;

        protected LeagueServiceBase(IDynamicRepository repository)
        {
            this.repository = repository;
        }

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

        private IEnumerable<PeriodGroup> GetPeriodGroupOfLeague(IEnumerable<LeagueMappingModel> enitiesByLeague, IDictionary<int, List<int>> monthGroups)
        {
            foreach (var year in monthGroups.Keys)
            {
                var entittiesByYear = enitiesByLeague.Where(e => e.Year == year).ToArray();
                yield return GetPeriodGroupBySumOf(entittiesByYear);

                foreach (var month in monthGroups[year])
                {
                    var entitiesByMonth = entittiesByYear.Where(e => e.MonthOrWeek == month).ToArray();
                    yield return GetPeriodGroupBySumOf(entitiesByMonth);
                }
            }

            yield return GetPeriodGroupBySumOf(enitiesByLeague);
        }

        private PeriodGroup GetPeriodGroupBySumOf(IEnumerable<LeagueMappingModel> enities)
        {
            return new PeriodGroup
            {
                BetCount = enities.Sum(e => e.BetCount),
                WinLoss = enities.Sum(e => e.WinLoss),
                Turnover = enities.Sum(e => e.Turnover)
            };
        }

        public abstract IEnumerable<string> GetGroupNames(IDictionary<int, List<int>> indexGroups);
    }
}