using Fanex.Data.Repository;
using PunterAnalysis.League.Model;

namespace PunterAnalysis.League
{
    public class LeagueServiceByMonth : LeagueServiceBase
    {
        public LeagueServiceByMonth(IDynamicRepository repository) : base(repository)
        {
        }

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
                foreach (var month in months)
                {
                    var beginOfMonth = new DateTime(year, month, 1);
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