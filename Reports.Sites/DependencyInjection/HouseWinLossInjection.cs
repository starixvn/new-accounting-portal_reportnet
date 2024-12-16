using Utilities.RemotingRef;
using BasicWinLoss = HouseWinLoss.Common.BasicWinLoss;
using CashOut = HouseWinLoss.CashOut;
using CommonServices = HouseWinLoss.Common.Services;
using Daily2hSoccerBettype = HouseWinLoss.DailyWinLoss2hSoccerMinor.BetType;
using Daily2hSoccerMinor = HouseWinLoss.DailyWinLoss2hSoccerMinor;
using Decorator = HouseWinLoss.Common.ColumnDecorators;
using MatchDetail = HouseWinLoss.Common.MatchDetail;
using MatchDetailByCode = HouseWinLoss.Common.MatchDetailByCode;
using MetricGaming = HouseWinLoss.MetricGaming;
using OtherSports = HouseWinLoss.OtherSports;
using Parlay = HouseWinLoss.Parlay;
using ProductBetType = HouseWinLoss.ProductBetType;
using Products = HouseWinLoss.Products;
using SimpleGrid = HouseWinLoss.SimpleGrid;
using SiteWinLoss = HouseWinLoss.Common.SiteWinLoss;
using Soccer = HouseWinLoss.Soccer;
using BetBuilder = HouseWinLoss.BetBuilder;


namespace Reports.Sites.DependencyInjection;

public static class HouseWinLossInjection
{
    public static void RegisterHouseWinLossServices(this IServiceCollection service)
    {
        service.AddTransient<RemotingRefService, RemotingRefServiceImp>();

        service.AddTransient<CommonServices.CalculationService, CommonServices.CalculationServiceImp>();
        service.AddTransient<CommonServices.RawWinLossService, CommonServices.RawWinLossServiceImp>();
        service.AddTransient<SiteWinLoss.WinLossService, SiteWinLoss.WinLossServiceImp>();
        service.AddTransient<SiteWinLoss.GridOptionsBuilder, SiteWinLoss.GridOptionsBuilderImp>();
        service.AddTransient<BasicWinLoss.BasicWinLossService, BasicWinLoss.BasicWinLossServiceImp>();
        service.AddTransient<BasicWinLoss.BasicGridOptionsBuilder, BasicWinLoss.BasicGridOptionsBuilderImp>();
        service.AddTransient<BasicWinLoss.BasicGridDataRowBuilder, BasicWinLoss.BasicGridDataRowBuilderImp>();
        service.AddTransient<MatchDetail.CalculationService, MatchDetail.CalculationServiceImp>();
        service.AddTransient<MatchDetail.RawWinLossService, MatchDetail.RawWinLossServiceImp>();
        service.AddTransient<MatchDetail.WinLossService, MatchDetail.WinLossServiceImp>();
        service.AddTransient<MatchDetail.GridOptionsBuilder, MatchDetail.GridOptionsBuilderImp>();
        service.AddTransient<MatchDetail.GetTeamNameAndSuffixService, MatchDetail.GetTeamNameAndSuffixServiceImp>();
        service.AddTransient<MatchDetailByCode.WinLossService, MatchDetailByCode.WinLossServiceImp>();
        service.AddTransient<MatchDetailByCode.GridOptionsBuilder, MatchDetailByCode.GridOptionsBuilderImp>();
        service.AddTransient<Products.Service, Products.ServiceImp>();
        service.AddTransient<Products.GridOptionsBuilder, Products.GridOptionsBuilderImp>();
        service.AddTransient<SimpleGrid.SimpleCriteriaFactory, SimpleGrid.SimpleCriteriaFactoryImp>();

        service.AddTransient<Soccer.GroupBetType.GroupBetTypeWinLossService, Soccer.GroupBetType.GroupBetTypeWinLossServiceImp>();
        service.AddTransient<Soccer.SumBetType.SumBetTypeWinLossService, Soccer.SumBetType.SumBetTypeWinLossServiceImp>();
        service.AddTransient<Soccer.GroupBetType.GroupBetTypeGridOptionsBuilder, Soccer.GroupBetType.GroupBetTypeGridOptionsBuilderImp>();
        service.AddTransient<Soccer.SumBetType.SumBetTypeGridOptionsBuilder, Soccer.SumBetType.SumBetTypeGridOptionsBuilderImp>();
        service.AddTransient<Soccer.BaseService.BaseCalculationService, Soccer.BaseService.BaseCalculationServiceImp>();
        service.AddTransient<Soccer.BetTypeFull.AbstractFactory, Soccer.BetTypeFull.AbstractFactoryImp>();
        service.AddTransient<Soccer.LiveNonLive.SoccerLiveNonLiveWinLossService, Soccer.LiveNonLive.SoccerLiveNonLiveWinLossServiceImp>();
        service.AddTransient<Parlay.Site.WinLossService, Parlay.Site.WinLossServiceImp>();
        service.AddTransient<Parlay.Site.GridOptionsBuilder, Parlay.Site.GridOptionsBuilderImp>();
        service.AddTransient<OtherSports.LiveNonLive.OtherSportsLiveNonLiveWinLossService, OtherSports.LiveNonLive.OtherSportsLiveNonLiveWinLossServiceImp>();
        service.AddTransient<MetricGaming.LiveNonLive.MetricGamingLiveNonLiveWinLossService, MetricGaming.LiveNonLive.MetricGamingLiveNonLiveWinLossServiceImp>();

        service.AddTransient<Decorator.GridOptionColumnDecorator, Decorator.GridOptionColumnDecoratorBasic>();
        service.AddTransient<Decorator.GridOptionColumnDecoratorBasic, Decorator.GridOptionColumnDecoratorBasic>();
        service.AddTransient<Decorator.GridOptionColumnDecoratorFull, Decorator.GridOptionColumnDecoratorFull>();
        service.AddTransient<Decorator.GridOptionColumnRemoval, Decorator.GridOptionColumnRemovalImp>();
        service.AddTransient<Decorator.MatchGridOptionColumnDecorator, Decorator.MatchGridOptionColumnDecoratorByCode>();
        service.AddTransient<Decorator.MatchGridOptionColumnDecoratorByCode, Decorator.MatchGridOptionColumnDecoratorByCode>();
        service.AddTransient<Decorator.MatchGridOptionColumnDecoratorFull, Decorator.MatchGridOptionColumnDecoratorFull>();
        service.AddTransient<Decorator.MatchGridOptionColumnDecoratorBasic, Decorator.MatchGridOptionColumnDecoratorBasic>();
        service.AddTransient<Decorator.MatchGridOptionColumnDecoratorOutright, Decorator.MatchGridOptionColumnDecoratorOutright>();

        service.AddTransient<CashOut.LiveNonLive.CashOutLiveNonLiveWinLossService, CashOut.LiveNonLive.CashOutLiveNonLiveWinLossServiceImp>();
        service.AddTransient<Daily2hSoccerBettype.BettypeGridOptionColumnDecorator, Daily2hSoccerBettype.BettypeGridOptionColumnDecoratorFull>();
        service.AddTransient<Daily2hSoccerMinor.MatchDetail.SoccerMinorMatchGridOptionColumnDecorator, Daily2hSoccerMinor.MatchDetail.SoccerMinorMatchGridOptionColumnDecoratorFull>();
        service.AddTransient<Daily2hSoccerMinor.OutrightMatchDetail.SoccerMinorMatchGridOptionColumnDecoratorOutright, Daily2hSoccerMinor.OutrightMatchDetail.SoccerMinorMatchGridOptionColumnDecoratorOutrightImp>();
        service.AddTransient<Daily2hSoccerMinor.MatchSortingService, Daily2hSoccerMinor.MatchSortingServiceImp>();

        service.AddTransient<ProductBetType.Service, ProductBetType.ServiceImp>();
        service.AddTransient<ProductBetType.GridOptionsBuilder, ProductBetType.GridOptionsBuilderImp>();

        service.AddTransient<BetBuilder.SportType.BetBuilderSportTypeWinLossService, BetBuilder.SportType.BetBuilderSportTypeWinLossServiceImp>();
    }
}