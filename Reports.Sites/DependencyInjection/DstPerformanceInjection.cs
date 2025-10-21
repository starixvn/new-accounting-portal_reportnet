using DstPerformance.Services;

namespace Reports.Sites.DependencyInjection;

public static class DstPerformanceInjection
{
    public static void RegisterDstPerformanceServices(this IServiceCollection service)
    {
        service.AddTransient<DstPerformanceService, DstPerformanceServiceImp>();
        service.AddTransient<ExcelBuilderService, ExcelBuilderServiceImp>();
    }
}