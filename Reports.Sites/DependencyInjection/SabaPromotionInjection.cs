namespace Reports.Sites.DependencyInjection;

using SabaPromotion.Services;

public static class SabaPromotionInjection
{
    public static void RegisterSabaPromotionServices(this IServiceCollection service)
    {
        service.AddTransient<SabaPromotionService, SabaPromotionServiceImp>();
        service.AddTransient<ExcelBuilderService, ExcelBuilderServiceImp>();
    }
}