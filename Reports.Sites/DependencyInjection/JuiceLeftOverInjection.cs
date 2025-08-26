
using Fanex.NAP.TGRS.Reports.Services.JuiceLeftOver;
using TGR.Services;
using TGR.Services.JuiceLeftOver;

namespace Reports.Sites.DependencyInjection;


public static class JuiceLeftOverInjection
{
    public static void RegisterJuiceLeftOverInjectionServices(this IServiceCollection service)
    {
        service.AddTransient<TeamService, TeamServiceImp>();
        service.AddTransient<BetTypeService, BetTypeServiceImp>();
        service.AddTransient<GradeService, GradeServiceImp>();
        service.AddTransient<LeagueService, LeagueServiceImp>();
        service.AddTransient<TraderService, TraderServiceImp>();
        service.AddTransient<DataService, DataServiceImp>();
        service.AddTransient<ExcelService, ExcelServiceImp>();
    }
}