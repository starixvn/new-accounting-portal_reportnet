using BetList.Services;

namespace Nap_Reports.Sites.DependencyInjection
{
    public static class BetListInjection
    {
        public static void RegisterBetListServices(this IServiceCollection service)
        {
            service.AddTransient<ScoreInformationService, ScoreInformationServiceImp>();
        }
    }
}
