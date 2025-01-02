using AutoMapper;
using CashOutPerformance.Criteria;
using CashOutPerformance.Models;
using CashOutPerformance.Requests;
using CashOutPerformance.ViewModels;

namespace CashOutPerformance;

public static class AutoMapperConfiguration
{
    public static void InitializeCashOutPerformance(this IProfileExpression cfg)
    {
        cfg.CreateMap<SportRequest, SportCriteria>();
        cfg.CreateMap<LicenseeRequest, LicenseeCriteria>();
        cfg.CreateMap<LeagueRequest, LeagueCriteria>();
        cfg.CreateMap<TicketRequest<SportLevelModel>, SportLevelCriteria>();
        cfg.CreateMap<TicketRequest<EventStatusLevelModel>, EventStatusLevelCriteria>();
        cfg.CreateMap<TicketRequest<BetTypeLevelModel>, BetTypeLevelCriteria>();
        cfg.CreateMap<TicketRequest<MatchDetailLevelModel>, MatchDetailLevelCriteria>();
        cfg.CreateMap<TicketExcelRequest<SportLevelModel>, SportLevelCriteria>();
        cfg.CreateMap<TicketExcelRequest<EventStatusLevelModel>, EventStatusLevelCriteria>();
        cfg.CreateMap<TicketExcelRequest<BetTypeLevelModel>, BetTypeLevelCriteria>();
        cfg.CreateMap<TicketExcelRequest<MatchDetailLevelModel>, MatchDetailLevelCriteria>();
        cfg.CreateMap<TicketViewModel<SportLevelModel>, TicketViewExcelModel<SportLevelExcelModel>>();
        cfg.CreateMap<TicketViewModel<EventStatusLevelModel>, TicketViewExcelModel<EventStatusLevelExcelModel>>();
        cfg.CreateMap<TicketViewModel<BetTypeLevelModel>, TicketViewExcelModel<BetTypeLevelExcelModel>>();
        cfg.CreateMap<TicketViewModel<MatchDetailLevelModel>, TicketViewExcelModel<MatchDetailLevelExcelModel>>();
        cfg.CreateMap<LeagueGroupRequest, LeagueGroupCriteria>();
        cfg.CreateMap<SportItemModel, SportItemExcelModel>();
        cfg.CreateMap<SportModel, SportExcelModel>();
        cfg.CreateMap<SportLevelModel, SportLevelExcelModel>();
        cfg.CreateMap<SportLevelRequest, SportLevelCriteria>();
        cfg.CreateMap<SportLevelExcelRequest, SportLevelCriteria>();
        cfg.CreateMap<SportLevelModel, SportLevelExcelModel>();
        cfg.CreateMap<EventStatusLevelModel, EventStatusLevelExcelModel>();
        cfg.CreateMap<BetTypeLevelModel, BetTypeLevelExcelModel>();
        cfg.CreateMap<MatchDetailLevelModel, MatchDetailLevelExcelModel>();
        cfg.CreateMap<TicketRequest<SimpleBetTypeLevelModel>, BetTypeLevelCriteria>();
        cfg.CreateMap<TicketExcelRequest<SimpleBetTypeLevelModel>, BetTypeLevelCriteria>();
        cfg.CreateMap<TicketViewModel<SimpleBetTypeLevelModel>, TicketViewExcelModel<SimpleBetTypeLevelExcelModel>>();
        cfg.CreateMap<BetTypeLevelModel, ChildItemBetTypeModel>();
        cfg.CreateMap<BetTypeLevelModel, BetTypeLevelExcelModel>();
        cfg.CreateMap<BetTypeDrillDownExcelRequest, GameTimeCriteria>();
        cfg.CreateMap<BetTypeDrillDownRequest, GameTimeCriteria>();
        cfg.CreateMap<BetTypeDrillDownExcelRequest, BetTypeLevelCriteria>();
        cfg.CreateMap<BetTypeDrillDownRequest, BetTypeLevelCriteria>();
        cfg.CreateMap<ChildItemBetTypeModel, BetTypeLevelExcelModel>();
        cfg.CreateMap<SimpleBetTypeLevelModel, SimpleBetTypeLevelExcelModel>();
    }
}