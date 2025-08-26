using TGR.Services;
using TGR.Services.SoccerPerformanceDetail;

namespace Reports.Sites.DependencyInjection;

public static class SoccerPerformanceDetailInjection
{
    public static void RegisterSoccerPerformanceDetailServices(this IServiceCollection service)
    {
        service.AddTransient<SoccerPerformanceDetailService, SoccerPerformanceDetailServiceImp>();
        service.AddTransient<ExcelService, ExcelServiceImp>();
        service.AddTransient<BetChoiceExcelService, BetChoiceExcelServiceImp>();
        service.AddTransient<LiveLeagueExcelService, LiveLeagueExcelServiceImp>();
        service.AddTransient<LeagueExcelService, LeagueExcelServiceImp>();
        service.AddTransient<TraderExcelService, TraderExcelServiceImp>();
        service.AddTransient<TeamExcelService, TeamExcelServiceImp>();
        service.AddTransient<GradeExcelService, GradeExcelServiceImp>();
        service.AddTransient<PermissionService, PermissionServiceImp>();
        service.AddTransient<DataService, DataServiceImp>();
    }
}