using TreeWinLoss.Services;

namespace Nap_Reports.Sites.DependencyInjection
{
    public static class TreeWinLossInjection
    {
        public static void RegisterTreeWinLossServices(this IServiceCollection service)
        {
            service.AddTransient<CriteriaFactory, CriteriaFactoryImp>();
            service.AddTransient<TreeWinLossService, TreeWinLossServiceImp>();
        }
    }
}
