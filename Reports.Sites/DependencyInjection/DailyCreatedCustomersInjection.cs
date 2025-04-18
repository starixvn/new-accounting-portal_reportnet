
namespace Reports.Sites.DependencyInjection;

using DailyCreatedCustomers.Services;

public static class DailyCreatedCustomersInjection
{
    public static void RegisterDailyCreatedCustomersServices(this IServiceCollection service)
    {
        service.AddTransient<DailyCreatedCustomersService, DailyCreatedCustomersServiceImp>();
        service.AddTransient<DailyCreatedCustomersExcelService, DailyCreatedCustomersExcelServiceImp>();
    }
}