using Fanex.Data.Repository;
using PunterAnalysis.League.Model;

namespace PunterAnalysis.League
{
    public class LeagueServiceByMonth(IDynamicRepository repository) : LeagueServiceBase(repository)
    {
        protected override LeagueMappingModel Transform(LeagueRawModel rawModel)
        {
            return new LeagueMappingModel
            {
                LeagueId = rawModel.LeagueId,
                LeagueName = rawModel.LeagueName,
                Year = rawModel.Year,
                MonthOrWeek = rawModel.Month,
                WinLoss = rawModel.WinLoss,
                Turnover = rawModel.Turnover,
                BetCount = rawModel.BetCount
            };
        }

        public override IEnumerable<string> GetGroupNames(IDictionary<int, List<int>> indexGroups)
        {
            foreach (var year in indexGroups.Keys)
            {
                yield return $"Year {year}";
                var months = indexGroups[year];
                foreach (var beginOfMonth in months.Select(month => new DateTime(year, month, 1)))
                {
                    yield return beginOfMonth.ToString("MMMM");
                }
            }

            if (indexGroups.Any())
            {
                yield return "Grand Total";
            }
        }
    }
}