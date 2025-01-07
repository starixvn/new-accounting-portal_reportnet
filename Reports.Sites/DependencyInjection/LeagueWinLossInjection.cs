using LeagueWinLoss.Services;

namespace Reports.Sites.DependencyInjection;

public static class LeagueWinLossInjection
{
    public static void RegisterLeagueWinLossServices(this IServiceCollection service)
    {
        service.AddTransient<LeagueWinLossService, LeagueWinLossServiceImp>();
        service.AddTransient<SortingService, SortingServiceImp>();
        service.AddTransient<LeagueWinLossExcelService, LeagueWinLossExcelServiceImp>();
        service.AddTransient<MatchWinLossExcelService, MatchWinLossExcelServiceImp>();
        service.AddTransient<BetTypeWinLossExcelService, BetTypeWinLossExcelServiceImp>();
        service.AddTransient<SiteWinLossExcelService, SiteWinLossExcelServiceImp>();
        service.AddTransient<SuperWinLossExcelService, SuperWinLossExcelServiceImp>();
    }
}