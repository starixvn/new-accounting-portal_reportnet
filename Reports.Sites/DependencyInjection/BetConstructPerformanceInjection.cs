using BetConstructPerformance.Services;

namespace Reports.Sites.DependencyInjection;

public static class BetConstructPerformanceInjection
{
    public static void RegisterBetConstructPerformanceServices(this IServiceCollection service)
    {
        service.AddTransient<BetConstructPerformanceService, BetConstructPerformanceServiceImp>();
        service.AddTransient<ExcelBuilderService, ExcelBuilderServiceImp>();
    }
}