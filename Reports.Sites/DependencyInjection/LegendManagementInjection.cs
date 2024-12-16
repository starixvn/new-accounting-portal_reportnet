using LegendManagement.Services;

namespace Reports.Sites.DependencyInjection;

public static class LegendManagementInjection
{
    public static void RegisterLegendManagementServices(this IServiceCollection service)
    {
        service.AddTransient<LegendManagementService, LegendManagementServiceImp>();
    }
}