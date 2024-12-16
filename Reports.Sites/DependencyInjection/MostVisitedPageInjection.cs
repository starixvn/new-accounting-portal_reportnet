using Utilities.MostVisitedPages;

namespace Nap_Reports.Sites.DependencyInjection
{
    public static class MostVisitedPageInjection
    {
        public static void RegisterMostVisitedPageServices(this IServiceCollection service)
        {
            service.AddTransient<MostVisitedPagesService, MostVisitedPagesServiceImp>();
            service.AddTransient<MenuService, MenuServiceImp>();
        }
    }
}
