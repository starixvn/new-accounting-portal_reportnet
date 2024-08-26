using DirectBalance.Service;

namespace Nap_Reports.Sites.DependencyInjection
{
    public static class DirectBalanceInjection
    {
        public static void RegisterDirectBalanceServices(this IServiceCollection service)
        {
            service.AddTransient<DirectBalanceService, DirectBalanceServiceImp>();
        }
    }
}
