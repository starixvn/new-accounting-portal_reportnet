using SMAWinLoss.Filter;
using SMAWinLoss.Summary;

namespace Reports.Sites.DependencyInjection;

using SMAWinLoss.VirtualRate;

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