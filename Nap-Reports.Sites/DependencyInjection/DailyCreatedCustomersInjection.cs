
using DailyCreatedCustomers.Services;

namespace Nap_Reports.Sites.DependencyInjection
{
    public static class DailyCreatedCustomersInjection
    {
        public static void RegisterDailyCreatedCustomersServices(this IServiceCollection service)
        {
            service.AddTransient<DailyCreatedCustomersService, DailyCreatedCustomersServiceImp>();
            service.AddTransient<DailyCreatedCustomersExcelService, DailyCreatedCustomersExcelServiceImp>();
        }
    }
}
