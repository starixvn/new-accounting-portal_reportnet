namespace Reports.Sites.DependencyInjection;

using TraderPerformance.Repositories;
using TraderPerformance.Services;

public static class TradePerformanceSoccerInjection
{
    public static void RegisterTradePerformanceSoccerServices(this IServiceCollection service)
    {
        service.AddTransient<TraderPerformanceSoccerRepository, TraderPerformanceSoccerRepositoryImp>();
        service.AddTransient<TraderPerformanceSoccerService, TraderPerformanceSoccerServiceImp>();
        service.AddTransient<TraderPerformanceSoccerExcelService, TraderPerformanceSoccerExcelServiceImp>();
        service.AddTransient<TraderPerformanceOtherSportsRepository, TraderPerformanceOtherSportsRepositoryImp>();
        service.AddTransient<TraderPerformanceOtherSportsService, TraderPerformanceOtherSportsServiceImp>();
        service.AddTransient<TraderPerformanceOtherSportsExcelService, TraderPerformanceOtherSportsExcelServiceImp>();
        service.AddTransient<TraderPerformanceSumPHExcelService, TraderPerformanceSumPHExcelServiceImp>();
        service.AddTransient<TraderSumPivotService, TraderSumPivotServiceImp>();
    }
}