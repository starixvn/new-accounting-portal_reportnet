using AutoMapper;
using HouseWinLoss.Common.Criteria;

namespace HouseWinLoss;

public static class AutoMapperConfiguration
{
    public static void InitializeHouseWinLoss(this IProfileExpression cfg)
    {
        cfg.CreateMap<Bullfighting.MatchDetail.QueryModel, UniversalThirdPartyMatchDetailCriteria>();
        cfg.CreateMap<Common.MatchDetail.RawWinLoss, Common.MatchDetail.WinLoss>();
        cfg.CreateMap<Common.Models.WinLossRaw, Common.Models.WinLoss>();
        cfg.CreateMap<MetricGaming.BetType.ExcelQueryModel, UniversalThirdPartyBetTypeCriteria>();
        cfg.CreateMap<MetricGaming.BetType.QueryModel, UniversalThirdPartyBetTypeCriteria>();
        cfg.CreateMap<MetricGaming.LiveNonLive.ExcelQueryModel, UniversalSecondLevelCriteria>();
        cfg.CreateMap<MetricGaming.LiveNonLive.QueryModel, UniversalSecondLevelCriteria>();
        cfg.CreateMap<MetricGaming.MatchDetail.ExcelQueryModel, UniversalThirdPartyMatchDetailCriteria>();
        cfg.CreateMap<MetricGaming.MatchDetail.QueryModel, UniversalThirdPartyMatchDetailCriteria>();
        cfg.CreateMap<MetricGaming.Site.ExcelQueryModel, UniversalThirdPartySiteCriteria>();
        cfg.CreateMap<MetricGaming.Site.QueryModel, UniversalThirdPartySiteCriteria>();
        cfg.CreateMap<NumberGame.MatchDetail.ExcelQueryModel, NumberGameDrilldownCriteria>();
        cfg.CreateMap<NumberGame.MatchDetail.QueryModel, NumberGameDrilldownCriteria>();
        cfg.CreateMap<OtherSports.BetType.ExcelQueryModel, OtherSports.BetType.Criteria>();
        cfg.CreateMap<OtherSports.BetType.QueryModel, OtherSports.BetType.Criteria>();
        cfg.CreateMap<OtherSports.LiveNonLive.ExcelQueryModel, OtherSports.SportType.Criteria>();
        cfg.CreateMap<OtherSports.LiveNonLive.QueryModel, OtherSports.LiveNonLive.Criteria>();
        cfg.CreateMap<OtherSports.SportType.ExcelQueryModel, OtherSports.SportType.Criteria>();
        cfg.CreateMap<OtherSports.SportType.QueryModel, OtherSports.SportType.Criteria>();
        cfg.CreateMap<Parlay.BetType.ExcelQueryModel, Parlay.BetType.Criteria>();
        cfg.CreateMap<Parlay.BetType.QueryModel, Parlay.BetType.Criteria>();
        cfg.CreateMap<Parlay.Site.ExcelQueryModel, Parlay.Site.Criteria>();
        cfg.CreateMap<Parlay.Site.QueryModel, Parlay.Site.Criteria>();
        cfg.CreateMap<Products.ExcelQueryModel, Products.Criteria>();
        cfg.CreateMap<Products.QueryModel, Products.Criteria>();
        cfg.CreateMap<Racing.MatchDetail.ExcelQueryModel, UniversalThirdPartyMatchDetailCriteria>();
        cfg.CreateMap<Racing.MatchDetail.QueryModel, UniversalThirdPartyMatchDetailCriteria>();
        cfg.CreateMap<Soccer.BetType.ExcelQueryModel, Soccer.BetType.BetTypeCriteria>();
        cfg.CreateMap<Soccer.BetType.QueryModel, Soccer.BetType.BetTypeCriteria>();
        cfg.CreateMap<Soccer.BetTypeFull.ExcelQueryModel, Soccer.SumBetType.SumBetTypeCriteria>();
        cfg.CreateMap<Soccer.BetTypeFull.ExcelQueryModel, Soccer.BetType.BetTypeCriteria>();
        cfg.CreateMap<Soccer.BetTypeFull.ExcelQueryModel, Soccer.GroupBetType.GroupBetTypeCriteria>();
        cfg.CreateMap<Soccer.GroupBetType.ExcelQueryModel, Soccer.GroupBetType.GroupBetTypeCriteria>();
        cfg.CreateMap<Soccer.GroupBetType.QueryModel, Soccer.GroupBetType.GroupBetTypeCriteria>();
        cfg.CreateMap<Soccer.LiveNonLive.ExcelQueryModel, Soccer.MajorMinor.Criteria>();
        cfg.CreateMap<Soccer.LiveNonLive.QueryModel, Soccer.LiveNonLive.Criteria>();
        cfg.CreateMap<Soccer.MajorMinor.ExcelQueryModel, Soccer.MajorMinor.Criteria>();
        cfg.CreateMap<Soccer.MajorMinor.QueryModel, Soccer.MajorMinor.Criteria>();
        cfg.CreateMap<Soccer.SumBetType.ExcelQueryModel, Soccer.SumBetType.SumBetTypeCriteria>();
        cfg.CreateMap<Soccer.SumBetType.QueryModel, Soccer.SumBetType.SumBetTypeCriteria>();
        cfg.CreateMap<Sportsbook.MatchDetail.ExcelQueryModel, UniversalSportbookMatchDetailCriteria>();
        cfg.CreateMap<Sportsbook.MatchDetail.QueryModel, UniversalSportbookMatchDetailCriteria>();
        cfg.CreateMap<Sportsbook.OutrightMatchDetail.ExcelQueryModel, UniversalSportbookMatchDetailCriteria>();
        cfg.CreateMap<Sportsbook.OutrightMatchDetail.QueryModel, UniversalSportbookMatchDetailCriteria>();
        cfg.CreateMap<Sportsbook.Site.ExcelQueryModel, UniversalSportbookSiteCriteria>();
        cfg.CreateMap<Sportsbook.Site.QueryModel, UniversalSportbookSiteCriteria>();

        cfg.CreateMap<NG2.BetType.ExcelLiveNonLiveQueryModel, UniversalThirdPartyBetTypeCriteria>();
        cfg.CreateMap<VirtualSports.RacingDetail.ExcelQueryModel, UniversalThirdPartyMatchDetailCriteria>();
        cfg.CreateMap<VirtualSports.RacingDetail.QueryModel, UniversalThirdPartyMatchDetailCriteria>();
        cfg.CreateMap<VirtualSports.SportsbookDetail.ExcelQueryModel, UniversalThirdPartyMatchDetailCriteria>();
        cfg.CreateMap<VirtualSports.SportsbookDetail.QueryModel, UniversalThirdPartyMatchDetailCriteria>();
        cfg.CreateMap<VirtualSportsBetradar.MatchDetail.ExcelQueryModel, UniversalThirdPartyMatchDetailCriteria>();
        cfg.CreateMap<VirtualSportsBetradar.MatchDetail.QueryModel, UniversalThirdPartyMatchDetailCriteria>();
        cfg.CreateMap<DailyWinLoss2hSoccerMinor.SportType.QueryModel, DailyWinLoss2hSoccerMinor.SportType.Criteria>();
        cfg.CreateMap<DailyWinLoss2hSoccerMinor.SportType.ExcelQueryModel, DailyWinLoss2hSoccerMinor.SportType.Criteria>();
        cfg.CreateMap<DailyWinLoss2hSoccerMinor.BetType.QueryModel, DailyWinLoss2hSoccerMinor.BetType.Criteria>();
        cfg.CreateMap<DailyWinLoss2hSoccerMinor.BetType.ExcelQueryModel, DailyWinLoss2hSoccerMinor.BetType.Criteria>();
        cfg.CreateMap<DailyWinLoss2hSoccerMinor.MatchDetail.QueryModel, UniversalSportbookMatchDetailCriteria>();
        cfg.CreateMap<DailyWinLoss2hSoccerMinor.MatchDetail.ExcelQueryModel, UniversalSportbookMatchDetailCriteria>();
        cfg.CreateMap<DailyWinLoss2hSoccerMinor.OutrightMatchDetail.QueryModel, UniversalSportbookMatchDetailCriteria>();
        cfg.CreateMap<DailyWinLoss2hSoccerMinor.OutrightMatchDetail.ExcelQueryModel, UniversalSportbookMatchDetailCriteria>();
        cfg.CreateMap<DailyWinLoss2hSoccerMinor.Site.QueryModel, DailyWinLoss2hSoccerMinor.Site.Criteria>();
        cfg.CreateMap<DailyWinLoss2hSoccerMinor.Site.ExcelQueryModel, DailyWinLoss2hSoccerMinor.Site.Criteria>();
        cfg.CreateMap<DailyWinLoss2hSoccerMinor.Site.QueryModel, UniversalSportbookSiteCriteria>();
        cfg.CreateMap<DailyWinLoss2hSoccerMinor.Site.ExcelQueryModel, UniversalSportbookSiteCriteria>();


        cfg.CreateMap<CashOut.LiveNonLive.ExcelQueryModel, UniversalSecondLevelCriteria>();
        cfg.CreateMap<CashOut.LiveNonLive.QueryModel, UniversalSecondLevelCriteria>();
        cfg.CreateMap<CashOut.MatchDetail.ExcelQueryModel, UniversalThirdPartyMatchDetailCriteria>();
        cfg.CreateMap<CashOut.MatchDetail.QueryModel, UniversalThirdPartyMatchDetailCriteria>();
        cfg.CreateMap<BetBuilder.SportType.ExcelQueryModel, UniversalSecondLevelCriteria>();
        cfg.CreateMap<BetBuilder.SportType.QueryModel, UniversalSecondLevelCriteria>();
        cfg.CreateMap<BetBuilder.BetType.ExcelQueryModel, UniversalThirdPartyBetTypeCriteria>();
        cfg.CreateMap<BetBuilder.BetType.QueryModel, UniversalThirdPartyBetTypeCriteria>();
        cfg.CreateMap<BetBuilder.Site.ExcelQueryModel, UniversalThirdPartySiteCriteria>();
        cfg.CreateMap<BetBuilder.Site.QueryModel, UniversalThirdPartySiteCriteria>();
        cfg.CreateMap<BetBuilder.MatchDetail.ExcelQueryModel, UniversalThirdPartyMatchDetailCriteria>();
        cfg.CreateMap<BetBuilder.MatchDetail.QueryModel, UniversalThirdPartyMatchDetailCriteria>();
        cfg.CreateMap<ProductBetType.ExcelQueryModel, ProductBetType.Criteria>();
        cfg.CreateMap<ProductBetType.QueryModel, ProductBetType.Criteria>();
    }
}