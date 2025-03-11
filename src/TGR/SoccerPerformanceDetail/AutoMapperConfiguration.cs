using SoccerPerformanceDetail.Model;
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
        cfg.CreateMap<BetTypePerformanceInfo, BetTypePerformanceItem>();
      //  cfg.CreateMap<List<BetTypePerformanceInfo>, List<BetTypePerformanceItem>>();

        cfg.CreateMap<BetTypePerformanceInfo, TraderPerformanceItem>();
      //  cfg.CreateMap<List<BetTypePerformanceInfo>, List<TraderPerformanceItem>>();
    }
}