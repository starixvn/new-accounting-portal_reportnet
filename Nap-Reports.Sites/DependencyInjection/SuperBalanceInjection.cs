using SuperBalance.Services;

namespace Nap_Reports.Sites.DependencyInjection
{
    public static class SuperBalanceInjection
    {
        public static void RegisterSuperBalanceServices(this IServiceCollection service)
        {
            service.AddTransient<SuperBalanceService, SuperBalanceServiceImp>();
            service.AddTransient<SuperBalanceExcelService, SuperBalanceExcelServiceImp>();
        }
    }
}
