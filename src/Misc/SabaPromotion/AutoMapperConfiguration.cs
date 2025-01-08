using AutoMapper;
using SabaPromotion.Models;

namespace SabaPromotion;

public static class AutoMapperConfiguration
{
    public static void InitializeSabaPromotion(this IProfileExpression cfg)
    {
        cfg.CreateMap<SabaPromotionItem, SabaPromotionExcelModel>();
    }
}