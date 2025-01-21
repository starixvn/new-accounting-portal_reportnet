using SabaPromotion.Services;

namespace Reports.Sites.DependencyInjection;

public static class SabaPromotionInjection
{
    public static void RegisterSabaPromotionServices(this IServiceCollection service)
    {
        service.AddTransient<SabaPromotionService, SabaPromotionServiceImp>();
        service.AddTransient<ExcelBuilderService, ExcelBuilderServiceImp>();
    }
}