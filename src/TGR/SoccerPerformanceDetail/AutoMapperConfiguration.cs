namespace SoccerPerformanceDetail;

using AutoMapper;
using ViewModel;

public static class AutoMapperConfiguration
{
    public static void InitializeSoccerPerformanceDetail(this IProfileExpression cfg)
    {
        cfg.CreateMap<BetTypePerformanceItem, BetTypePerformanceExcelItem>();
        cfg.CreateMap<BetTypeBetChoiceItem, BetTypeBetChoiceExcelItem>();
        cfg.CreateMap<LeaguePerformanceItem, LeaguePerformanceExcelItem>();
    }
}