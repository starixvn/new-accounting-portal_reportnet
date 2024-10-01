using CreditMovementMonitor.Services;

namespace Nap_Reports.Sites.DependencyInjection
{
    public static class CreditMovementInjection
    {
        public static void RegisterCreditMovementServices(this IServiceCollection service)
        {
            service.AddTransient<CreditMovementService, CreditMovementServiceImp>();
        }
    }
}
