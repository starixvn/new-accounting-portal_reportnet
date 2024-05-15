using Fanex.Data.Repository;
using PunterAnalysis.Shared;

namespace PunterAnalysis.League
{
    public interface LeagueServiceFactory
    {
        LeagueService CreateLeagueService(int periodType);
    }

    public class LeagueServiceFactoryImp : LeagueServiceFactory
    {
        private readonly IDynamicRepository repository;

        private readonly DateTimeService dateTimeService;

        public LeagueServiceFactoryImp(IDynamicRepository repository, DateTimeService dateTimeService)
        {
            this.repository = repository;
            this.dateTimeService = dateTimeService;
        }

        public LeagueService CreateLeagueService(int periodType)
        {
            var periodTypeEnum = (PeriodType)periodType;

            switch (periodTypeEnum)
            {
                case PeriodType.Weekly:
                    return new LeagueServiceByWeek(repository, dateTimeService);

                case PeriodType.Annually:
                case PeriodType.Daily:
                case PeriodType.Monthly:
                case PeriodType.MonthRange:
                    return new LeagueServiceByMonth(repository);

                default:
                    throw new NotImplementedException();
            }
        }
    }
}