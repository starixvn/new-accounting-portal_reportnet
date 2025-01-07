using AutoMapper;
using LeagueWinLoss.Model;

namespace LeagueWinLoss;

public static class AutoMapperConfiguration
{
    public static void InitializeLeagueWinLoss(this IProfileExpression cfg)
    {
        cfg.CreateMap<LeagueWinLossItem, LeagueWinLossItemExcelModel>();
        cfg.CreateMap<MatchWinLossByLiveStatus, MatchWinLossItemExcelModel>();
        cfg.CreateMap<BettypeWinLossBySite, BetTypeWinLossItemExcelModel>();
        cfg.CreateMap<SiteWinLossItem, SiteWinLossItemExcelModel>();
        cfg.CreateMap<SuperWinLossItem, SuperWinLossItemExcelModel>();
    }
}