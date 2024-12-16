using TreeWinLoss.Services;

namespace Reports.Sites.DependencyInjection;

public static class TreeWinLossInjection
{
    public static void RegisterTreeWinLossServices(this IServiceCollection service)
    {
        service.AddTransient<CriteriaFactory, CriteriaFactoryImp>();
        service.AddTransient<TreeWinLossService, TreeWinLossServiceImp>();
        service.AddTransient<TreeWinLossExcelService, TreeWinLossExcelServiceImp>();
    }
}