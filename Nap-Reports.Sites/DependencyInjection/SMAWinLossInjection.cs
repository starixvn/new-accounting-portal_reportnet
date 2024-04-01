using SMAWinLoss.Filter;
using SMAWinLoss.Summary;
using UniversalReports.SMAWinLoss.VirtualRate;

namespace Nap_Reports.Sites.DependencyInjection
{
    public static class SMAWinLossInjection
    {
        public static void RegisterSMAWinLossServices(this IServiceCollection service)
        {
            service.AddTransient<FilterService, FilterServiceImp>();
            service.AddTransient<WinLossService, WinLossServiceImp>();
            service.AddTransient<VirtualRateService, VirtualRateServiceImp>();
            service.AddTransient<ExcelBuilder, ExcelBuilderImp>();
        }
    }
}
