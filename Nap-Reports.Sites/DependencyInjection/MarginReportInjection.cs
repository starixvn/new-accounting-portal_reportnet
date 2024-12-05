using Margin.Services;

namespace Nap_Reports.Sites.DependencyInjection
{
    public static class MarginReportInjection
    {
        public static void RegisterMarginReportServices(this IServiceCollection service)
        {
            service.AddTransient<MarginWinLossService, MarginWinLossServiceImp>();
            service.AddTransient<MarginWinLossExcelService, MarginWinLossExcelServiceImp>();
        }
    }
}
