using CreditMovementMonitor.Services;

namespace Reports.Sites.DependencyInjection;

public static class CreditMovementInjection
{
    public static void RegisterCreditMovementServices(this IServiceCollection service)
    {
        service.AddTransient<CreditMovementService, CreditMovementServiceImp>();
    }
}