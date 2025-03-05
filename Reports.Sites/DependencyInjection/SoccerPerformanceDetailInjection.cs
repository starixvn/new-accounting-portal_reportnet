using SoccerPerformanceDetail.Services;

namespace Reports.Sites.DependencyInjection;

public static class SoccerPerformanceDetailInjection
{
    public static void RegisterSoccerPerformanceDetailServices(this IServiceCollection service)
    {
        service.AddTransient<SoccerPerformanceDetailService, SoccerPerformanceDetailServiceImp>();
        service.AddTransient<ExcelService, ExcelServiceImp>();
        service.AddTransient<BetChoiceExcelService, BetChoiceExcelServiceImp>();
    }
}