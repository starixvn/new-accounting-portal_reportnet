

using BetCount.Services;

namespace Nap_Reports.Sites.DependencyInjection
{
    public static class BetCountInjection
    {
        public static void RegisterBetCountServices(this IServiceCollection service)
        {
            service.AddTransient<BetCountService, BetCountServiceImp>();
            service.AddTransient<BetCountExportExcelService, BetCountExportExcelServiceImp>();
        }
    }
}
