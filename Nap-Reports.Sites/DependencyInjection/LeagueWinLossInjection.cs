using LeagueWinLoss.Repositories;
using LeagueWinLoss.Services;

namespace Nap_Reports.Sites.DependencyInjection
{
    public static class LeagueWinLossInjection
    {
        public static void RegisterLeagueWinLossServices(this IServiceCollection service)
        {
            service.AddTransient<LeagueWinLossRepository, LeagueWinLossRepositoryImp>();
            service.AddTransient<LeagueWinLossService, LeagueWinLossServiceImp>();
            service.AddTransient<SortingService, SortingServiceImp>();
        }
    }
}
