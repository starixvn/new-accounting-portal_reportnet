namespace Reports.Sites.DependencyInjection;

using Margin.Services;

public static class MarginReportInjection
{
    public static void RegisterMarginReportServices(this IServiceCollection service)
    {
        service.AddTransient<MarginWinLossService, MarginWinLossServiceImp>();
        service.AddTransient<MarginWinLossExcelService, MarginWinLossExcelServiceImp>();
    }
}