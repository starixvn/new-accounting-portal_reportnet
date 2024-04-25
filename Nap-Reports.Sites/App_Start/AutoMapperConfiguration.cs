using AbnormalBet;
using ActualMemberBet;
using AutoMapper;
using CashOutPerformance;
using Exchange;
using HouseWinLoss;
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
                cfg.InitializeHouseWinLoss();
                cfg.InitializeSMAWinLoss();
                cfg.InitializeSBPerformance();
                cfg.InitializeAbnormalBet();
                cfg.InitializeActualMemberBet();
                cfg.InitializeExchange();
                cfg.InitializeCashOutPerformance();
                cfg.InitializeUpdatedUsernameCredit();
            });

            var mapper = config.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}