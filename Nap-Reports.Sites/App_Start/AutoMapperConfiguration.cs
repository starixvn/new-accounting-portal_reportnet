using AbnormalBet;
using ActualMemberBet;
using AutoMapper;
using CashOutPerformance;
using Exchange;
using HouseWinLoss;
using PunterAnalysis;
using SBCustomerPerformanceByDanger;
using SBPerformance;
using SMAWinLoss;
using UpdatedUsernameCredit;
using DailyMixParlay;
using ResultAnalysis;

namespace Nap_Reports.Sites.App_Start
{
    public static class AutoMapperConfiguration
    {
        public static void Initialize(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.InitializeHouseWinLoss();
                cfg.InitializeSMAWinLoss();
                cfg.InitializeSBPerformance();
                cfg.InitializeAbnormalBet();
                cfg.InitializeActualMemberBet();
                cfg.InitializeExchange();
                cfg.InitializeCashOutPerformance();
                cfg.InitializeUpdatedUsernameCredit();
                cfg.InitializeSBCustomerPerformanceByDanger();
                cfg.InitializePunterAnalysis();
                cfg.InitializeParlay();
                cfg.InitializeResultAnalysis();
            });

            var mapper = config.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}