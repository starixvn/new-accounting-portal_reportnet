using AutoMapper;
using Nap_Reports.HouseWinLoss;
using SBPerformance;
using SMAWinLoss;

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
            });

            var mapper = config.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}