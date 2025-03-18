namespace SoccerPerformanceDetail;

using AutoMapper;
using SoccerPerformanceDetail.Model;
using ViewModel;

public static class AutoMapperConfiguration
{
    public static void InitializeSoccerPerformanceDetail(this IProfileExpression cfg)
    {
        cfg.CreateMap<BetTypePerformanceItem, BetTypePerformanceExcelItem>();
        cfg.CreateMap<BetTypeBetChoiceItem, BetTypeBetChoiceExcelItem>();
        cfg.CreateMap<LeaguePerformanceItem, LeaguePerformanceExcelItem>();
        cfg.CreateMap<BetTypePerformanceInfo, BetTypePerformanceItem>();
        cfg.CreateMap<BetTypeBetChoicePerformanceInfo, BetTypeBetChoiceItem>();

        cfg.CreateMap<TraderPerformanceInfo, TraderPerformanceItem>();
        cfg.CreateMap<LeaguePerformanceInfo, LeaguePerformanceItem>();
        cfg.CreateMap<MatchPerformanceRawInfo, MatchPerformanceItem>();

        cfg.CreateMap<MatchPerformanceInfo, MatchPerformanceItem>();
    }
}