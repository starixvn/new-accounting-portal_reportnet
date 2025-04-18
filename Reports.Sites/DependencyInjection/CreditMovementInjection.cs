namespace Reports.Sites.DependencyInjection;

using CreditMovementMonitor.Services;

public static class CreditMovementInjection
{
    public static void RegisterCreditMovementServices(this IServiceCollection service)
    {
        service.AddTransient<CreditMovementService, CreditMovementServiceImp>();
    }
}