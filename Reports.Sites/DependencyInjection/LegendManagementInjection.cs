using LegendManagement.Services;

namespace Nap_Reports.Sites.DependencyInjection;

public static class LegendManagementInjection
{
    public static void RegisterLegendManagementServices(this IServiceCollection service)
    {
        service.AddTransient<LegendManagementService, LegendManagementServiceImp>();
    }
}