using BetList.Services;

namespace Reports.Sites.DependencyInjection;

public static class BetListInjection
{
    public static void RegisterBetListServices(this IServiceCollection service)
    {
        service.AddTransient<ScoreInformationService, ScoreInformationServiceImp>();
    }
}