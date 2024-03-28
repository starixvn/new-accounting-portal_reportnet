using Nap_Reports.Utilities.RemotingRef;
using BasicWinLoss = Nap_Reports.HouseWinLoss.Common.BasicWinLoss;
using CashOut = Nap_Reports.HouseWinLoss.CashOut;
using CommonServices = Nap_Reports.HouseWinLoss.Common.Services;
using Daily2hSoccerBettype = Nap_Reports.HouseWinLoss.DailyWinLoss2hSoccerMinor.BetType;
using Daily2hSoccerMinor = Nap_Reports.HouseWinLoss.DailyWinLoss2hSoccerMinor;
using Decorator = Nap_Reports.HouseWinLoss.Common.ColumnDecorators;
using MatchDetail = Nap_Reports.HouseWinLoss.Common.MatchDetail;
using MatchDetailByCode = Nap_Reports.HouseWinLoss.Common.MatchDetailByCode;
using MetricGaming = Nap_Reports.HouseWinLoss.MetricGaming;
using OtherSports = Nap_Reports.HouseWinLoss.OtherSports;
using Parlay = Nap_Reports.HouseWinLoss.Parlay;
using ProductBetType = Nap_Reports.HouseWinLoss.ProductBetType;
using Products = Nap_Reports.HouseWinLoss.Products;
using SimpleGrid = Nap_Reports.HouseWinLoss.SimpleGrid;
using SiteWinLoss = Nap_Reports.HouseWinLoss.Common.SiteWinLoss;
using Soccer = Nap_Reports.HouseWinLoss.Soccer;

namespace Nap_Reports.Sites.DependencyInjection
{
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
        }
    }
}
