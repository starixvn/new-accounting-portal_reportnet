using AbnormalBet;
using ActualMemberBet;
using AutoMapper;
using CashOutPerformance;
using DailyCreatedCustomers;
using DailyMixParlay;
using DirectBalance;
using Exchange;
using HouseWinLoss;
using NegativeBalance;
using PunterAnalysis;
using ResultAnalysis;
using SBCustomerPerformanceByDanger;
using SBPerformance;
using SMAWinLoss;
using SuperBalance;
using TreeWinLoss;
using UpdatedUsernameCredit;
using BetCount;

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
                cfg.InitializeTreeWinLoss();
                cfg.InitializeNegativeBalance();
                cfg.InitializeSuperBalance();
                cfg.InitializeDirectBalance();
                cfg.InitializeDailyCreatedCustomers();
                cfg.InitializeBetCount();
            });

            var mapper = config.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}