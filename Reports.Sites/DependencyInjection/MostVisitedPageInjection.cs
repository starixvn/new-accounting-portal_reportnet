namespace Reports.Sites.DependencyInjection;

using Utilities.MostVisitedPages;

public static class MostVisitedPageInjection
{
    public static void RegisterMostVisitedPageServices(this IServiceCollection service)
    {
        service.AddTransient<MostVisitedPagesService, MostVisitedPagesServiceImp>();
        service.AddTransient<MenuService, MenuServiceImp>();
    }
}