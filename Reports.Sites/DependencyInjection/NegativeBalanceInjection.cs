using NegativeBalance.Services;

namespace Reports.Sites.DependencyInjection;

public static class NegativeBalanceInjection
{
    public static void RegisterNegativeBalanceServices(this IServiceCollection service)
    {
        service.AddTransient<NegativeBalanceService, NegativeBalanceServiceImp>();
        service.AddTransient<NegativeBalanceExcelService, NegativeBalanceExcelServiceImp>();
    }
}