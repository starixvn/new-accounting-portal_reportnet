

namespace Reports.Sites.DependencyInjection;

using BetCount.Services;

public static class BetCountInjection
{
    public static void RegisterBetCountServices(this IServiceCollection service)
    {
        service.AddTransient<BetCountService, BetCountServiceImp>();
        service.AddTransient<BetCountExportExcelService, BetCountExportExcelServiceImp>();
    }
}