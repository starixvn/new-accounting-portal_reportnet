using TraderPerformanceSoccer.Repositories;
using TraderPerformanceSoccer.Services;

namespace Nap_Reports.Sites.DependencyInjection
{
    public static class TradePerformanceSoccerInjection
    {
        public static void RegisterTradePerformanceSoccerServices(this IServiceCollection service)
        {
            service.AddTransient<TraderPerformanceSoccerRepository, TraderPerformanceSoccerRepositoryImp>();
            service.AddTransient<TraderPerformanceSoccerService, TraderPerformanceSoccerServiceImp>();
        }
    }
}
