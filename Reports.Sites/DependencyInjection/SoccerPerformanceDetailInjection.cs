namespace Reports.Sites.DependencyInjection;

using SoccerPerformanceDetail.Services;

public static class SoccerPerformanceDetailInjection
{
    public static void RegisterSoccerPerformanceDetailServices(this IServiceCollection service)
    {
        service.AddTransient<SoccerPerformanceDetailService, SoccerPerformanceDetailServiceImp>();
        service.AddTransient<ExcelService, ExcelServiceImp>();
        service.AddTransient<BetChoiceExcelService, BetChoiceExcelServiceImp>();
        service.AddTransient<LiveLeagueExcelService, LiveLeagueExcelServiceImp>();
    }
}