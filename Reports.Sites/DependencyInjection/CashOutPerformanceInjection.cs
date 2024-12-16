using CashOutPerformance.Models;
using CashOutPerformance.Services;

namespace Reports.Sites.DependencyInjection;

public static class CashOutPerformanceInjection
{
    public static void RegisterCashOutPerformanceServices(this IServiceCollection service)
    {
        service.AddTransient<CashOutPerformanceService, CashOutPerformanceServiceImp>();
        service.AddTransient<TicketsExcelBuilder<SportLevelExcelModel>, TicketsExcelBuilderImp<SportLevelExcelModel>>();
        service.AddTransient<TicketsExcelBuilder<EventStatusLevelExcelModel>, TicketsExcelBuilderImp<EventStatusLevelExcelModel>>();
        service.AddTransient<TicketsExcelBuilder<BetTypeLevelExcelModel>, TicketsExcelBuilderImp<BetTypeLevelExcelModel>>();
        service.AddTransient<TicketsExcelBuilder<MatchDetailLevelExcelModel>, TicketsExcelBuilderImp<MatchDetailLevelExcelModel>>();
    }
}