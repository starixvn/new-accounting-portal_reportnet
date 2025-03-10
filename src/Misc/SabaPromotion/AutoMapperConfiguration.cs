namespace SabaPromotion;

using AutoMapper;
using Models;

public static class AutoMapperConfiguration
{
    public static void InitializeSabaPromotion(this IProfileExpression cfg)
    {
        cfg.CreateMap<SabaPromotionItem, SabaPromotionExcelModel>();
    }
}