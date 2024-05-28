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

namespace Nap_Reports.Sites.App_Start
{
    public static class AutoMapperConfiguration
    {
        public static void Initialize(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AllowNullCollections = true;
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
            });

            var mapper = config.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}