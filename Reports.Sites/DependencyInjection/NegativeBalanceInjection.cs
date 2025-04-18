namespace Reports.Sites.DependencyInjection;

using NegativeBalance.Services;

public static class NegativeBalanceInjection
{
    public static void RegisterNegativeBalanceServices(this IServiceCollection service)
    {
        service.AddTransient<NegativeBalanceService, NegativeBalanceServiceImp>();
        service.AddTransient<NegativeBalanceExcelService, NegativeBalanceExcelServiceImp>();
    }
}