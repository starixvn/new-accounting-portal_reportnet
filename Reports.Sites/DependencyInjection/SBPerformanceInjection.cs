using SBPerformance.Main;
using SBPerformance.MarginBySite;
using SBPerformance.MarginBySport;
using SBPerformance.TopLosingLeague;
using SBPerformance.TopWinningMember;

namespace Reports.Sites.DependencyInjection;

public static class SBPerformanceInjection
{
    public static void RegisterSBPerformanceServices(this IServiceCollection service)
    {
        service.AddTransient<MainService, MainServiceImp>();
        service.AddTransient<MarginBySiteService, MarginBySiteServiceImp>();
        service.AddTransient<MarginBySportService, MarginBySportServiceImp>();
        service.AddTransient<TopLosingLeagueService, TopLosingLeagueServiceImp>();
        service.AddTransient<TopWinningMemberService, TopWinningMemberServiceImp>();
    }
}