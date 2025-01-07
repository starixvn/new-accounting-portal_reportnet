using AbnormalBet;
using ActualMemberBet;
using BetConstructPerformance;
using CashOutPerformance;
using Exchange;
using PunterAnalysis.Shared;
using ResultAnalysis;
using SBCustomerPerformanceByDanger;
using SBPerformance;
using SMAWinLoss;
using System.Reflection;
using UpdatedUsernameCredit;

namespace Reports.Sites.DependencyInjection;

public static class MediatRInjection
{
    public static void AddAllMediatR(this IServiceCollection service)
    {
        service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SMAWinLossAttribute).GetTypeInfo().Assembly));
        service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SBPerformanceAttribute).GetTypeInfo().Assembly));
        service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AbnormalBetAttribute).GetTypeInfo().Assembly));
        service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ActualMemberBetAttribute).GetTypeInfo().Assembly));
        service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ExchangeAttribute).GetTypeInfo().Assembly));
        service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CashOutPerformanceAttribute).GetTypeInfo().Assembly));
        service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdatedUsernameAndCreditAttribute).GetTypeInfo().Assembly));
        service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SBCustomerPerformanceByDangerAttribute).GetTypeInfo().Assembly));
        service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(PunterAnalysisAttribute).GetTypeInfo().Assembly));
        service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ResultAnalysisAttribute).GetTypeInfo().Assembly));
        service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(BetConstructPerformanceAttribute).GetTypeInfo().Assembly));
    }
}