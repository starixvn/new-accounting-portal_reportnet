using AutoMapper;
using TraderPerformance.Model;

namespace TraderPerformance;

public static class AutoMapperConfiguration
{
    public static void InitializeTraderPerformance(this IProfileExpression cfg)
    {
        cfg.CreateMap<TraderPerformanceOtherSportsModel, TraderPerformanceOtherSportsExcelModel>();
    }
}