namespace Reports.Sites.DependencyInjection;

using DailyMixParlay.Services;

public static class DailyMixParlayInjection
{
    public static void RegisterDailyMixParlayServices(this IServiceCollection service)
    {
        service.AddTransient<DailyMixParlayWinLossService, DailyMixParlayWinLossServiceImp>();
        service.AddTransient<DailyMixParlayWinLossExcelService, DailyMixParlayWinLossExcelServiceImp>();
    }
}