using DirectBalance.Services;

namespace Reports.Sites.DependencyInjection;

public static class DirectBalanceInjection
{
    public static void RegisterDirectBalanceServices(this IServiceCollection service)
    {
        service.AddTransient<DirectBalanceService, DirectBalanceServiceImp>();
        service.AddTransient<DirectBalanceExcelService, DirectBalanceExcelServiceImp>();
    }
}