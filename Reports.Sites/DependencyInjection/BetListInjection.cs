namespace Reports.Sites.DependencyInjection;

using BetList.Services;

public static class BetListInjection
{
    public static void RegisterBetListServices(this IServiceCollection service)
    {
        service.AddTransient<ScoreInformationService, ScoreInformationServiceImp>();
    }
}