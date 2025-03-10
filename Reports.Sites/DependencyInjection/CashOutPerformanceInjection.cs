namespace Reports.Sites.DependencyInjection;

using CashOutPerformance.Models;
using CashOutPerformance.Services;

public static class CashOutPerformanceInjection
{
    public static void RegisterCashOutPerformanceServices(this IServiceCollection service)
    {
        service.AddTransient<CashOutPerformanceService, CashOutPerformanceServiceImp>();
        service.AddTransient<TicketsExcelBuilder<SportLevelExcelModel>, TicketsExcelBuilderImp<SportLevelExcelModel>>();
        service.AddTransient<TicketsExcelBuilder<EventStatusLevelExcelModel>, TicketsExcelBuilderImp<EventStatusLevelExcelModel>>();
        service.AddTransient<TicketsExcelBuilder<BetTypeLevelExcelModel>, TicketsExcelBuilderImp<BetTypeLevelExcelModel>>();
        service.AddTransient<TicketsExcelBuilder<MatchDetailLevelExcelModel>, TicketsExcelBuilderImp<MatchDetailLevelExcelModel>>();
        service.AddTransient<ModelsHelperService, ModelsHelperServiceImp>();
        service.AddTransient<TicketsExcelBuilder<SimpleBetTypeLevelExcelModel>, TicketsExcelBuilderImp<SimpleBetTypeLevelExcelModel>>();
    }
}