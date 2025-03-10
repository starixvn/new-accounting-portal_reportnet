namespace Reports.Sites.DependencyInjection;

using SuperBalance.Services;

public static class SuperBalanceInjection
{
    public static void RegisterSuperBalanceServices(this IServiceCollection service)
    {
        service.AddTransient<SuperBalanceService, SuperBalanceServiceImp>();
        service.AddTransient<SuperBalanceExcelService, SuperBalanceExcelServiceImp>();
    }
}