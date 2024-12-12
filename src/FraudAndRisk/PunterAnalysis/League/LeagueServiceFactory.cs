using Fanex.Data.Repository;
using PunterAnalysis.Shared;

namespace PunterAnalysis.League
{
    public interface LeagueServiceFactory
    {
        LeagueService CreateLeagueService(int periodType);
    }

    public class LeagueServiceFactoryImp(IDynamicRepository repository, DateTimeService dateTimeService)
        : LeagueServiceFactory
    {
        public LeagueService CreateLeagueService(int periodType)
        {
            var periodTypeEnum = (PeriodType)periodType;

            return periodTypeEnum switch
            {
                PeriodType.Weekly => new LeagueServiceByWeek(repository, dateTimeService),
                PeriodType.Annually or PeriodType.Daily or PeriodType.Monthly or PeriodType.MonthRange =>
                    new LeagueServiceByMonth(repository),
                _ => throw new NotImplementedException()
            };
        }
    }
}