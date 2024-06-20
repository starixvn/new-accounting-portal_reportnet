using DailyMixParlayWinLoss.Services;

namespace Nap_Reports.Sites.DependencyInjection
{
    public static class DailyMixParlayInjection
    {
        public static void RegisterDailyMixParlayServices(this IServiceCollection service)
        {
            service.AddTransient<DailyMixParlayWinLossService, DailyMixParlayWinLossServiceImp>();
        }
    }
}
