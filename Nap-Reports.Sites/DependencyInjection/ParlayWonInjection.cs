using ParlayWon.Services;

namespace Nap_Reports.Sites.DependencyInjection
{
    public static class ParlayWonInjection
    {
        public static void RegisterParlayWonServices(this IServiceCollection service)
        {
            service.AddTransient<ParlayWonService, ParlayWonServiceImp>();
        }
    }
}
