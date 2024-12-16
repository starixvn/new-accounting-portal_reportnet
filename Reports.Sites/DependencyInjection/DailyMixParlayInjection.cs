using DailyMixParlay.Services;

namespace Reports.Sites.DependencyInjection;

public static class DailyMixParlayInjection
{
    public static void RegisterDailyMixParlayServices(this IServiceCollection service)
    {
        service.AddTransient<DailyMixParlayWinLossService, DailyMixParlayWinLossServiceImp>();
        service.AddTransient<DailyMixParlayWinLossExcelService, DailyMixParlayWinLossExcelServiceImp>();
    }
}