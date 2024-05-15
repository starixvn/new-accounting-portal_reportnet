using Fanex.Data.Repository;
using PunterAnalysis.League.Model;
using PunterAnalysis.Shared;

namespace PunterAnalysis.League
{
    public class LeagueServiceByWeek : LeagueServiceBase
    {
        private readonly DateTimeService dateTimeService;

        public LeagueServiceByWeek(IDynamicRepository repository, DateTimeService dateTimeService) : base(repository)
        {
            this.dateTimeService = dateTimeService;
        }

        protected override LeagueMappingModel Transform(LeagueRawModel rawModel)
        {
            return new LeagueMappingModel
            {
                LeagueId = rawModel.LeagueId,
                LeagueName = rawModel.LeagueName,
                Year = rawModel.Year,
                MonthOrWeek = rawModel.Week,
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
                var weeks = indexGroups[year];
                foreach (var week in weeks)
                {
                    var beginOfWeek = dateTimeService.GetFirstDateOfWeekIso8601(year, week);
                    yield return $"Week {week} ({beginOfWeek:MMM dd, yyyy})";
                }
            }

            if (indexGroups.Any())
            {
                yield return "Grand Total";
            }
        }
    }
}