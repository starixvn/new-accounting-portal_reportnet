using AutoMapper;
using TGR.Model.SoccerPerformanceDetail;
using TGR.ViewModel.SoccerPerformanceDetail;
using JuiceLeftOver = TGR.Model.JuiceLeftOver;
using JuiceLeftOverViewModel = TGR.ViewModel.JuiceLeftOver;

namespace TGR;

public static class AutoMapperConfiguration
{
    public static void InitializeSoccerPerformanceDetail(this IProfileExpression cfg)
    {
        cfg.CreateMap<BetTypePerformanceItem, BetTypePerformanceExcelItem>();
        cfg.CreateMap<BetTypeBetChoiceItem, BetTypeBetChoiceExcelItem>();
        cfg.CreateMap<LeaguePerformanceItem, LeaguePerformanceExcelItem>();
        cfg.CreateMap<LeaguePerformanceItem, LiveLeaguePerformanceExcelItem>();
        cfg.CreateMap<BetTypePerformanceInfo, BetTypePerformanceItem>();
        cfg.CreateMap<BetTypeBetChoicePerformanceInfo, BetTypeBetChoiceItem>();
        cfg.CreateMap<TraderPerformanceInfo, TraderPerformanceItem>();
        cfg.CreateMap<LeaguePerformanceInfo, LeaguePerformanceItem>();
        cfg.CreateMap<MatchPerformanceRawInfo, MatchPerformanceItem>();
        cfg.CreateMap<MatchPerformanceInfo, MatchPerformanceItem>();
        cfg.CreateMap<BetTypeDetailInfo, BetTypeDetailItem>();
        cfg.CreateMap<TraderPerformanceItem, TraderPerformanceExcelItem>();
        cfg.CreateMap<TeamPerformanceInfo, TeamPerformanceItem>();
        cfg.CreateMap<TeamPerformanceItem, TeamPerformanceExcelItem>();
        cfg.CreateMap<GradePerformanceInfo, GradePerformanceItem>();
        cfg.CreateMap<GradePerformanceItem, GradePerformanceExcelItem>();
        cfg.CreateMap<PointHDPPerformanceInfo, PointHDPPerformanceItem>();
    }

    public static void InitializePerformanceDetailJuiceLeftOver(this IProfileExpression cfg)
    {
        cfg.CreateMap<JuiceLeftOver.BetTypePerformanceInfo, JuiceLeftOverViewModel.BetTypePerformanceInfoExcelItem>();
        cfg.CreateMap<JuiceLeftOver.LeaguePerformanceInfo, JuiceLeftOverViewModel.LeaguePerformanceInfoExcelItem>();
        cfg.CreateMap<JuiceLeftOver.TeamPerformanceInfo, JuiceLeftOverViewModel.TeamPerformanceInfoExcelItem>();
    }
}