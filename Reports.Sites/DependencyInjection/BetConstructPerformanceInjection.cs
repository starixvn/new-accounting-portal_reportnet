namespace Reports.Sites.DependencyInjection;

using BetConstructPerformance.Services;

public static class BetConstructPerformanceInjection
{
    public static void RegisterBetConstructPerformanceServices(this IServiceCollection service)
    {
        service.AddTransient<BetConstructPerformanceService, BetConstructPerformanceServiceImp>();
        service.AddTransient<ExcelBuilderService, ExcelBuilderServiceImp>();
    }
}