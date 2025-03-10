namespace Reports.Sites.DependencyInjection;

using LegendManagement.Services;

public static class LegendManagementInjection
{
    public static void RegisterLegendManagementServices(this IServiceCollection service)
    {
        service.AddTransient<LegendManagementService, LegendManagementServiceImp>();
    }
}